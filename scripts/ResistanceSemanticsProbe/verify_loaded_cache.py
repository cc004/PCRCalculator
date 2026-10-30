"""Verify actual loaded resistance caches against the explicit selected resistance database."""
import argparse,json,sqlite3
from pathlib import Path
p=argparse.ArgumentParser();p.add_argument('--db','--cn-db',dest='db',type=Path,required=True);p.add_argument('caches',type=Path,nargs='+');a=p.parse_args()
c=sqlite3.connect('file:'+str(a.db.resolve())+'?mode=ro',uri=True)
metadata={str(i):(action,detail)for i,action,detail in c.execute('select ailment_id,ailment_action,ailment_detail_1 from ailment_data')}
columns=[r[1]for r in c.execute('pragma table_info(resist_data)')if r[1].startswith('ailment_')and r[1][8:].isdigit()];columns.sort(key=lambda x:int(x[8:]));maximum=max(int(x[8:])for x in columns);expected={}
for row in c.execute('select resist_status_id,'+','.join(columns)+' from resist_data'):
 values=[0]*maximum
 for col,value in zip(columns,row[1:]):values[int(col[8:])-1]=int(value or 0)
 expected[str(row[0])]=values
for path in a.caches:
 cache=json.loads(path.read_text());actual={k:(v['ailment_action'],v['ailment_detail_1'])for k,v in cache['ailmentDataDic'].items()}
 assert actual==metadata,(str(path),'metadata differs from selected source')
 assert cache['resistDataDic']==expected,(str(path),'resistance values differ from selected source')
 print(json.dumps({'cache':str(path),'schema':cache['cacheSchemaVersion'],'metadata_count':len(actual),'resistance_ids':len(expected),'source_exact_match':True}))
