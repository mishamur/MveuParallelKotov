using Log;
namespace ArchiveFiles
{
    public delegate bool CancelArch();
    public class Archiver
    {
        CancelArch cancelArch;
        Logger logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public Archiver(Logger logger = null)
        {
            this.cancelArch = () => false;
            this.logger = logger;
        }

        /// <summary>
        /// прекращает архивацию
        /// </summary>
        public void CancelArchive()
        {
            cancelArch = () => true;
        }
            
        /// <summary>
        /// Архивирует файлы в данной директории
        /// </summary>
        /// <param name="pathToFolder">путь к директории</param>
        public Task<bool> ArchiveFolder(string pathToFolder)
        {
            if (Directory.Exists(pathToFolder))
            {
                CancellationTokenSource sourceToken = new CancellationTokenSource();
                CancellationToken token = sourceToken.Token;

                TaskFactory taskFactory = new TaskFactory(token);

                string[] files = Directory.GetFiles(pathToFolder);
               
                   
               //имитация архивирование файла
                Random rnd = new Random();
                taskFactory.StartNew(() =>
                {
                foreach (string file in files)
                    {
                        Thread.Sleep(rnd.Next(1000, 3500));
                        logger?.Log("Файл обработан");
                      
                        //обработка esc
                        if (cancelArch?.Invoke() == true)
                        {
                            logger?.Log("Преждевременное завершение обработки");
                            sourceToken.Cancel();
                            token.ThrowIfCancellationRequested();
                              
                        }
                    }
                    logger?.Log("Архивирование окончено, нажмите любую кнопку");
                });
                return Task.FromResult(true);

            }
            else
            {
                logger?.Log("Указанной директории не существует");
            }
            return Task.FromResult(false);
        }
    }
}