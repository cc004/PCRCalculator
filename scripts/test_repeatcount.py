"""验证固定种子下单次模拟与 RepeatCount 展开的输入和结果一致。

先启动已准备好游戏数据的服务端，再执行
    python scripts/test_repeatcount.py http://127.0.0.1:8080
"""

import copy
import json
from pathlib import Path
import sys
import unittest
import urllib.request


class RepeatCountTests(unittest.TestCase):
    def setUp(self):
        if not getattr(self, 'url', None):
            self.skipTest('请通过脚本参数提供已启动的服务端地址')

    def simulate(self, seed, count):
        payload = copy.deepcopy(self.payload)
        payload[0]['RepeatCount'] = count
        payload[0]['Settings']['UseFixedRandomSeed'] = True
        payload[0]['Settings']['RandomSeed'] = seed
        request = urllib.request.Request(
            self.url + '/simulate',
            data=json.dumps(payload).encode('utf-8'),
            headers={'Content-Type': 'application/json'},
        )
        opener = urllib.request.build_opener(urllib.request.ProxyHandler({}))
        with opener.open(request, timeout=180) as response:
            results = json.load(response)
        self.assertEqual(len(results), 1)
        self.assertTrue(results[0]['Success'], results[0].get('Message'))
        return results[0]

    def test_fixed_seed_repeat_preserves_inputs_and_results(self):
        for seed in (666, 2026, 12345):
            with self.subTest(seed=seed):
                single = self.simulate(seed, 1)
                repeated = self.simulate(seed, 10)
                self.assertEqual(repeated['CurrentSeedList'], [seed] * 10)
                self.assertEqual(repeated['PlayerList'], single['PlayerList'])
                self.assertEqual(repeated['EnemyList'], single['EnemyList'])
                self.assertEqual(repeated['ResultList'], [single['Result']] * 10)
                self.assertEqual(repeated['BattleFramesList'], [single['BattleFrames']] * 10)


if __name__ == '__main__':
    RepeatCountTests.url = sys.argv.pop(1).rstrip('/') if len(sys.argv) > 1 else 'http://127.0.0.1:8080'
    fixture = Path(__file__).with_name('fixtures') / 'repeatcount-arena.json'
    RepeatCountTests.payload = json.loads(fixture.read_text(encoding='utf-8'))
    unittest.main()
