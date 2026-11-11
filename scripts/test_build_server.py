import importlib.util
from pathlib import Path
import tempfile
import unittest
from unittest.mock import patch
import subprocess

spec = importlib.util.spec_from_file_location('build_server', Path(__file__).with_name('build_server.py'))
build_server = importlib.util.module_from_spec(spec)
spec.loader.exec_module(build_server)

class ServerPackageTests(unittest.TestCase):
    def test_fresh_publish_preserves_previous_package_and_does_not_bundle_its_data(self):
        with tempfile.TemporaryDirectory() as tmp:
            root = Path(tmp)
            old = root / 'Linux/publish'
            old.mkdir(parents=True)
            (old / 'jp10067700.db').write_text('old')
            def publish(command, **kwargs):
                self.assertIn('linux-x64', command)
                target = Path(command[-1])
                target.mkdir()
                (target / 'PCRCalculatorServer').write_text('new')
            with patch('sys.argv', ['build_server', '--target', 'linux', '--output-root', tmp]), patch.object(build_server.subprocess, 'run', side_effect=publish):
                build_server.main()
            self.assertTrue((root / 'mzq-linux-x64.zip').is_file())
            self.assertFalse((old / 'jp10067700.db').exists())
            self.assertEqual(len(list(root.glob('Linux-previous-*/Linux/publish/jp10067700.db'))), 1)

    def test_publish_failure_leaves_previous_package_intact(self):
        with tempfile.TemporaryDirectory() as tmp:
            target = Path(tmp) / 'Linux'
            target.mkdir()
            (target / 'previous').write_text('intact')
            with patch('sys.argv', ['build_server', '--target', 'linux', '--output-root', tmp]), patch.object(build_server.subprocess, 'run', side_effect=subprocess.CalledProcessError(1, 'dotnet')):
                with self.assertRaises(subprocess.CalledProcessError):
                    build_server.main()
            self.assertEqual((target / 'previous').read_text(), 'intact')

if __name__ == '__main__':
    unittest.main()
