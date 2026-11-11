from pathlib import Path
import tempfile,subprocess,json
here=Path(__file__).resolve().parent
repo=Path(__file__).resolve().parents[2] if (Path(__file__).resolve().parents[2]/'Assets').exists() else Path('/Users/aoi/Projects/PCRCalculator')
def method(s,signature):
 a=s.index(signature);i=s.index('{',a)+1;depth=1
 while depth:depth+=(s[i]=='{')-(s[i]=='}');i+=1
 return s[a:i]
with tempfile.TemporaryDirectory(prefix='pcr-rng-contract-') as d:
 root=Path(d)
 for case in ['Resistance','Effects']:
  p=root/case;p.mkdir();(p/'Probe.csproj').write_text('<Project Sdk="Microsoft.NET.Sdk"><PropertyGroup><OutputType>Exe</OutputType><TargetFramework>net8.0</TargetFramework><ImplicitUsings>enable</ImplicitUsings></PropertyGroup></Project>');(p/'NuGet.Config').write_text('<configuration><packageSources><clear /></packageSources></configuration>');(p/'UnityRandom.cs').write_bytes((repo/'Assets/Scrips/UnityRandom.cs').read_bytes())
  program=(here/(case+'Program.cs')).read_text()
  if case=='Resistance':
   source=(repo/'Assets/Scrips/Elements/BasePartsData.cs').read_text();body='\n'.join(method(source,s)for s in ['private void createResistStatus(','public bool ResistStatus(']);(p/'GameParts.cs').write_text('using System;using System.Collections.Generic;using System.Linq;public class GameParts:BasePartsData{'+body+'public void Build(int id){current=id;createResistStatus(id);}}');(p/'Stubs.cs').write_bytes((here/'ResistanceStubs.cs').read_bytes())
  else:
   source=(repo/'Assets/Scrips/Elements/UnitActionController_add.cs').read_text();m=method(source,'private void createNormalEffectPrefab2(');guard=m[m.index('            if ('):m.index('            GameObject prefab1')].strip();a=program.index('if (_skillEffect');b=program.index('switch(_skillEffect',a);program=program[:a]+guard+'\n'+program[b:];a=m.index('case eEffectBehavior.SKILL_AREA_RANDOM:');b=m.index('break;',a)+6;pa=program.index('case eEffectBehavior.SKILL_AREA_RANDOM:');pb=program.index('break;',pa)+6;program=program[:pa]+m[a:b]+program[pb:];program=program.replace('Console.WriteLine(JsonSerializer.Serialize(new{','if(mismatches!=0 || !skippedStateUnchanged || stoppedCalls!=0 || fiveValidDispatches!=10 || normal.Length!=2 || normal[0][0]!=0 || normal[0][1]!=1 || normal[1][0]!=-1 || normal[1][1]!=1)throw new Exception("effect RNG contract mismatch");Console.WriteLine(JsonSerializer.Serialize(new{')
  (p/'Program.cs').write_text(program);subprocess.run(['dotnet','run','--project',str(p/'Probe.csproj')],check=True)

# Extract the exact decision block, including both draws, from FearAction.
with tempfile.TemporaryDirectory(prefix="pcr-fear-contract-") as d:
 p=Path(d);source=(repo/'Assets/Scrips/Elements/FearAction.cs').read_text();start=source.index('      var firstRandomData');end=source.index('\n      {',source.index('      if (num1',start));block=source[start:end];block=block.replace('      if (num1', '      return (num1')+';'
 helpers=(repo/'Assets/Scrips/Elements/BattleUtil.cs').read_text();helper_lines='\n'.join(x.strip() for x in helpers.splitlines() if 'public static bool Approximately(' in x or 'public static bool LessThanOrApproximately(' in x)
 (p/'Probe.csproj').write_text('<Project Sdk="Microsoft.NET.Sdk"><PropertyGroup><OutputType>Exe</OutputType><TargetFramework>net8.0</TargetFramework><ImplicitUsings>enable</ImplicitUsings></PropertyGroup></Project>');(p/'NuGet.Config').write_text('<configuration><packageSources><clear /></packageSources></configuration>');(p/'UnityRandom.cs').write_bytes((repo/'Assets/Scrips/UnityRandom.cs').read_bytes());(p/'Program.cs').write_text((here/'FearProgram.cs').read_text().replace('// SOURCE_BLOCK',block).replace('// SOURCE_HELPERS',helper_lines));subprocess.run(['dotnet','run','--project',str(p/'Probe.csproj')],check=True)
