#!/usr/bin/env python3
"""Package the existing .NET server target; no Unity, account save or database download."""
import argparse
from pathlib import Path
import shutil
import subprocess
import tempfile


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--target', choices=['linux', 'macos-server'], required=True)
    parser.add_argument('--output-root', type=Path, required=True)
    parser.add_argument('--framework-dependent', action='store_true')
    parser.add_argument('--no-zip', action='store_true')
    args = parser.parse_args()
    project = Path(__file__).resolve().parents[1]
    folder, runtime = {'linux': ('Linux', 'linux-x64'), 'macos-server': ('MacOSServer', 'osx-arm64')}[args.target]
    output = args.output_root.resolve()
    output.mkdir(parents=True, exist_ok=True)
    # A fresh publish prevents stale files (including databases/saves) entering a new package.
    staging = Path(tempfile.mkdtemp(prefix=folder + '-build-', dir=output))
    subprocess.run(['dotnet', 'publish', str(project / 'PCRCalculatorServer/PCRCalculatorServer.csproj'),
                    '-c', 'Release', '-r', runtime, '--self-contained', str(not args.framework_dependent).lower(),
                    '-p:PublishSingleFile=true', '-o', str(staging / 'publish')], check=True, cwd=project)
    target = output / folder
    if target.exists():
        backup = Path(tempfile.mkdtemp(prefix=folder + '-previous-', dir=output))
        target.rename(backup / folder)
        print(f'Previous package preserved: {backup / folder}')
    staging.rename(target)
    if not args.no_zip:
        shutil.make_archive(str(output / ('mzq-' + runtime)), 'zip', target)
    print(f'Server package: {target}; configured databases are acquired on application initialization.')


if __name__ == '__main__':
    main()
