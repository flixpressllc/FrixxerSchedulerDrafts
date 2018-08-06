using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// Class that makes it easy to read and write files. This is well-suited for text file manipulation for now.
/// There will be other writing capabilities added here in the future.
/// </summary>
namespace Frixxer.PresenterConsoleApp
{
    public class FileProcessor
    {
        public delegate void ErrorHandler(object s, FileProcessorErrorEventArgs e);
        public event ErrorHandler IOError;

        public void Write(string locationWithFileName, string contents)
        {
            StreamWriter SW = null; 
            try
            {
                SW = new StreamWriter(locationWithFileName);
                SW.Write(contents);              
            }
            catch (Exception ee)
            {
                if (IOError != null)
                {
                    FileProcessorErrorEventArgs x = new FileProcessorErrorEventArgs(ee.Message);
                    
                    IOError(null, x);
                }
            }
            finally
            {
                if (SW != null)
                {
                    SW.Close();
                }
            }
        }

        public void Write(string locationWithFileName, List<string> Lines)
        {
            StreamWriter SW = null;
            try
            {
                SW = new StreamWriter(locationWithFileName);
                
                foreach (string s in Lines)
                {
                    SW.WriteLine(s);
                }

            }
            catch (Exception ee)
            {
                if (IOError != null)
                {
                    FileProcessorErrorEventArgs x = new FileProcessorErrorEventArgs(ee.Message);

                    IOError(null, x);
                }  
            }
            finally
            {
                if (SW != null)
                {
                    SW.Close();
                }
            }
        }
        
        public void AppendLine(string locationWithFileName, string line)
        { 
            try
            {
                File.AppendAllLines(locationWithFileName, new List<string> { line });
            }
            catch(Exception ee)
            {
                if (IOError != null)
                {
                    FileProcessorErrorEventArgs x = new FileProcessorErrorEventArgs(ee.Message);

                    IOError(null, x);
                }
            }
            
        }

        public string ReadToString(string locationWithFileName)
        {
            string toReturn = "";
            StreamReader SR = null;

            try
            {
                SR = new StreamReader(locationWithFileName);
                toReturn = SR.ReadToEnd();

            }
            catch (Exception ee)
            {
                if (IOError != null)
                {
                    FileProcessorErrorEventArgs x = new FileProcessorErrorEventArgs(ee.Message);

                    IOError(null, x);
                }
            }
            finally
            {
                if (SR != null)
                {
                    SR.Close();
                }
            }
            return toReturn;
        }

        public List<string> ReadLine(string locationWithFileName)
        {
            List<string> Lines = new List<string>();
            StreamReader SR = null;
            try
            {
                SR = new StreamReader(locationWithFileName);

                while (!SR.EndOfStream)
                {
                    Lines.Add(SR.ReadLine());
                }


            }
            catch (Exception ee)
            {
                if (IOError != null)
                {
                    FileProcessorErrorEventArgs x = new FileProcessorErrorEventArgs(ee.Message);

                    IOError(null, x);
                }
            }
            finally
            {
                if (SR != null)
                {
                    SR.Close();
                }
            }

            return Lines;
        }

        public void Write(string fileName, byte[] buff)
        {
            
            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(buff);
                bw.Close(); //Thanks Karlo for pointing out!
               
            }
            catch (Exception ex)
            {
                if (IOError != null)
                {
                    FileProcessorErrorEventArgs x = new FileProcessorErrorEventArgs(ex.Message);

                    IOError(null, x);
                }
            }
                       
        }

        public byte[] ReadToByteArray(string fileName)
	    {
	        byte[] _Buffer = null;
	 
	        try
	        {
	            // Open file for reading
	            System.IO.FileStream _FileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
	         
	            // attach filestream to binary reader
	            System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);
	         
	            // get total byte length of the file
	            long _TotalBytes = new System.IO.FileInfo(fileName).Length;
	         
	            // read entire file into buffer
	            _Buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);
	         
	            // close file reader
	            _FileStream.Close();
	            _FileStream.Dispose();
	            _BinaryReader.Close();
	        }
	        catch (Exception ee)
	        {
                if (IOError != null)
                {
                    FileProcessorErrorEventArgs x = new FileProcessorErrorEventArgs(ee.Message);

                    IOError(null, x);
                }
	        }
	 
	        return _Buffer;
	    }

    }

    public class FileProcessorErrorEventArgs : EventArgs
    {
        private string _Message;

        public FileProcessorErrorEventArgs(string message) : base()
        {
            _Message = message;
        }

        public string Message
        {
            get { return _Message; }
        }
    }
}