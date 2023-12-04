using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

namespace PCRCaculator.Update
{
    /*
    public class ZipTool
    {
        #region ZipCallback
        public abstract class ZipCallback
        {
            /// <summary>
            /// ѹ�������ļ����ļ���ǰִ�еĻص�
            /// </summary>
            /// <param name="_entry"></param>
            /// <returns>�������true����ѹ���ļ����ļ��У���֮��ѹ���ļ����ļ���</returns>
            public virtual bool OnPreZip(ZipEntry _entry)
            {
                return true;
            }

            /// <summary>
            /// ѹ�������ļ����ļ��к�ִ�еĻص�
            /// </summary>
            /// <param name="_entry"></param>
            public virtual void OnPostZip(ZipEntry _entry) { }

            /// <summary>
            /// ѹ��ִ����Ϻ�Ļص�
            /// </summary>
            /// <param name="_result">true��ʾѹ���ɹ���false��ʾѹ��ʧ��</param>
            public virtual void OnFinished(bool _result) { }
        }
        #endregion

        #region UnzipCallback
        public abstract class UnzipCallback
        {
            /// <summary>
            /// ��ѹ�����ļ����ļ���ǰִ�еĻص�
            /// </summary>
            /// <param name="_entry"></param>
            /// <returns>�������true����ѹ���ļ����ļ��У���֮��ѹ���ļ����ļ���</returns>
            public virtual bool OnPreUnzip(ZipEntry _entry)
            {
                return true;
            }

            /// <summary>
            /// ��ѹ�����ļ����ļ��к�ִ�еĻص�
            /// </summary>
            /// <param name="_entry"></param>
            public virtual void OnPostUnzip(ZipEntry _entry) { }

            /// <summary>
            /// ��ѹִ����Ϻ�Ļص�
            /// </summary>
            /// <param name="_result">true��ʾ��ѹ�ɹ���false��ʾ��ѹʧ��</param>
            public virtual void OnFinished(bool _result) { }
        }
        #endregion

        /// <summary>
        /// ѹ���ļ����ļ���
        /// </summary>
        /// <param name="_fileOrDirectoryArray">�ļ���·�����ļ���</param>
        /// <param name="_outputPathName">ѹ��������·���ļ���</param>
        /// <param name="_password">ѹ������</param>
        /// <param name="_zipCallback">ZipCallback���󣬸���ص�</param>
        /// <returns></returns>
        public static bool Zip(string[] _fileOrDirectoryArray, string _outputPathName, string _password = null, ZipCallback _zipCallback = null)
        {
            if ((null == _fileOrDirectoryArray) || string.IsNullOrEmpty(_outputPathName))
            {
                if (null != _zipCallback)
                    _zipCallback.OnFinished(false);

                return false;
            }

            ZipOutputStream zipOutputStream = new ZipOutputStream(File.Create(_outputPathName));
            zipOutputStream.SetLevel(6);    // ѹ��������ѹ���ٶȵ�ƽ���
            if (!string.IsNullOrEmpty(_password))
                zipOutputStream.Password = _password;

            for (int index = 0; index < _fileOrDirectoryArray.Length; ++index)
            {
                bool result = false;
                string fileOrDirectory = _fileOrDirectoryArray[index];
                if (Directory.Exists(fileOrDirectory))
                    result = ZipDirectory(fileOrDirectory, string.Empty, zipOutputStream, _zipCallback);
                else if (File.Exists(fileOrDirectory))
                    result = ZipFile(fileOrDirectory, string.Empty, zipOutputStream, _zipCallback);

                if (!result)
                {
                    if (null != _zipCallback)
                        _zipCallback.OnFinished(false);

                    return false;
                }
            }

            zipOutputStream.Finish();
            zipOutputStream.Close();

            if (null != _zipCallback)
                _zipCallback.OnFinished(true);

            return true;
        }

        /// <summary>
        /// ��ѹZip��
        /// </summary>
        /// <param name="_filePathName">Zip�����ļ�·����</param>
        /// <param name="_outputPath">��ѹ���·��</param>
        /// <param name="_password">��ѹ����</param>
        /// <param name="_unzipCallback">UnzipCallback���󣬸���ص�</param>
        /// <returns></returns>
        public static bool UnzipFile(string _filePathName, string _outputPath, string _password = null, UnzipCallback _unzipCallback = null)
        {
            if (string.IsNullOrEmpty(_filePathName) || string.IsNullOrEmpty(_outputPath))
            {
                if (null != _unzipCallback)
                    _unzipCallback.OnFinished(false);

                return false;
            }

            try
            {
                return UnzipFile(File.OpenRead(_filePathName), _outputPath, _password, _unzipCallback);
            }
            catch (System.Exception _e)
            {
                Debug.LogError("[ZipUtility.UnzipFile]: " + _e.ToString());

                if (null != _unzipCallback)
                    _unzipCallback.OnFinished(false);

                return false;
            }
        }

        /// <summary>
        /// ��ѹZip��
        /// </summary>
        /// <param name="_fileBytes">Zip���ֽ�����</param>
        /// <param name="_outputPath">��ѹ���·��</param>
        /// <param name="_password">��ѹ����</param>
        /// <param name="_unzipCallback">UnzipCallback���󣬸���ص�</param>
        /// <returns></returns>
        public static bool UnzipFile(byte[] _fileBytes, string _outputPath, string _password = null, UnzipCallback _unzipCallback = null)
        {
            if ((null == _fileBytes) || string.IsNullOrEmpty(_outputPath))
            {
                if (null != _unzipCallback)
                    _unzipCallback.OnFinished(false);

                return false;
            }

            bool result = UnzipFile(new MemoryStream(_fileBytes), _outputPath, _password, _unzipCallback);
            if (!result)
            {
                if (null != _unzipCallback)
                    _unzipCallback.OnFinished(false);
            }

            return result;
        }

        /// <summary>
        /// ��ѹZip��
        /// </summary>
        /// <param name="_inputStream">Zip��������</param>
        /// <param name="_outputPath">��ѹ���·��</param>
        /// <param name="_password">��ѹ����</param>
        /// <param name="_unzipCallback">UnzipCallback���󣬸���ص�</param>
        /// <returns></returns>
        public static bool UnzipFile(Stream _inputStream, string _outputPath, string _password = null, UnzipCallback _unzipCallback = null)
        {
            if ((null == _inputStream) || string.IsNullOrEmpty(_outputPath))
            {
                if (null != _unzipCallback)
                    _unzipCallback.OnFinished(false);

                return false;
            }

            // �����ļ�Ŀ¼
            if (!Directory.Exists(_outputPath))
                Directory.CreateDirectory(_outputPath);

            // ��ѹZip��
            ZipEntry entry = null;
            using (ZipInputStream zipInputStream = new ZipInputStream(_inputStream))
            {
                if (!string.IsNullOrEmpty(_password))
                    zipInputStream.Password = _password;

                while (null != (entry = zipInputStream.GetNextEntry()))
                {
                    if (string.IsNullOrEmpty(entry.Name))
                        continue;

                    if ((null != _unzipCallback) && !_unzipCallback.OnPreUnzip(entry))
                        continue;   // ����

                    string filePathName = Path.Combine(_outputPath, entry.Name);

                    // �����ļ�Ŀ¼
                    if (entry.IsDirectory)
                    {
                        Directory.CreateDirectory(filePathName);
                        continue;
                    }

                    // д���ļ�
                    try
                    {
                        using (FileStream fileStream = File.Create(filePathName))
                        {
                            byte[] bytes = new byte[1024];
                            while (true)
                            {
                                int count = zipInputStream.Read(bytes, 0, bytes.Length);
                                if (count > 0)
                                    fileStream.Write(bytes, 0, count);
                                else
                                {
                                    if (null != _unzipCallback)
                                        _unzipCallback.OnPostUnzip(entry);

                                    break;
                                }
                            }
                        }
                    }
                    catch (System.Exception _e)
                    {
                        Debug.LogError("[ZipUtility.UnzipFile]: " + _e.ToString());

                        if (null != _unzipCallback)
                            _unzipCallback.OnFinished(false);

                        return false;
                    }
                }
            }

            if (null != _unzipCallback)
                _unzipCallback.OnFinished(true);

            return true;
        }

        #region PrivateMethod
        /// <summary>
        /// ѹ���ļ�
        /// </summary>
        /// <param name="_filePathName">�ļ�·����</param>
        /// <param name="_parentRelPath">Ҫѹ�����ļ��ĸ�����ļ���</param>
        /// <param name="_zipOutputStream">ѹ�������</param>
        /// <param name="_zipCallback">ZipCallback���󣬸���ص�</param>
        /// <returns></returns>
        private static bool ZipFile(string _filePathName, string _parentRelPath, ZipOutputStream _zipOutputStream, ZipCallback _zipCallback = null)
        {

            //Crc32 crc32 = new Crc32();
            ZipEntry entry = null;
            FileStream fileStream = null;
            try
            {
                string entryName = _parentRelPath + '/' + Path.GetFileName(_filePathName);
                entry = new ZipEntry(entryName);
                entry.DateTime = System.DateTime.Now;

                if ((null != _zipCallback) && !_zipCallback.OnPreZip(entry))
                    return true;    // ����

                fileStream = File.OpenRead(_filePathName);
                byte[] buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, buffer.Length);
                fileStream.Close();

                entry.Size = buffer.Length;

                //crc32.Reset();
                //crc32.Update(buffer);
                //entry.Crc = crc32.Value;

                _zipOutputStream.PutNextEntry(entry);
                _zipOutputStream.Write(buffer, 0, buffer.Length);
            }
            catch (System.Exception _e)
            {
                Debug.LogError("[ZipUtility.ZipFile]: " + _e.ToString());
                return false;
            }
            finally
            {
                if (null != fileStream)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }

            if (null != _zipCallback)
                _zipCallback.OnPostZip(entry);

            return true;
        }

        /// <summary>
        /// ѹ���ļ���
        /// </summary>
        /// <param name="_path">Ҫѹ�����ļ���</param>
        /// <param name="_parentRelPath">Ҫѹ�����ļ��еĸ�����ļ���</param>
        /// <param name="_zipOutputStream">ѹ�������</param>
        /// <param name="_zipCallback">ZipCallback���󣬸���ص�</param>
        /// <returns></returns>
        private static bool ZipDirectory(string _path, string _parentRelPath, ZipOutputStream _zipOutputStream, ZipCallback _zipCallback = null)
        {

            ZipEntry entry = null;
            try
            {
                string entryName = Path.Combine(_parentRelPath, Path.GetFileName(_path) + '/');
                entry = new ZipEntry(entryName);
                entry.DateTime = System.DateTime.Now;
                entry.Size = 0;

                if ((null != _zipCallback) && !_zipCallback.OnPreZip(entry))
                    return true;    // ����

                _zipOutputStream.PutNextEntry(entry);
                _zipOutputStream.Flush();

                string[] files = Directory.GetFiles(_path);
                for (int index = 0; index < files.Length; ++index)
                {
                    // �ų�Unity�п��ܵ� .meta �ļ�
                    if (files[index].EndsWith(".meta") == true)
                    {
                        Debug.LogWarning(files[index] + " not to zip");
                        continue;
                    }

                    ZipFile(files[index], Path.Combine(_parentRelPath, Path.GetFileName(_path)), _zipOutputStream, _zipCallback);
                }
            }
            catch (System.Exception _e)
            {
                Debug.LogError("[ZipUtility.ZipDirectory]: " + _e.ToString());
                return false;
            }

            string[] directories = Directory.GetDirectories(_path);
            for (int index = 0; index < directories.Length; ++index)
            {
                if (!ZipDirectory(directories[index], Path.Combine(_parentRelPath, Path.GetFileName(_path)), _zipOutputStream, _zipCallback))
                {
                    return false;
                }
            }

            if (null != _zipCallback)
                _zipCallback.OnPostZip(entry);

            return true;
        }
        #endregion
    }

    */
}