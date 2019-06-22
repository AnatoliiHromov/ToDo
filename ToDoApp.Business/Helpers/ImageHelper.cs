//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Web;

//namespace AdBoard.Helpers
//{
//    public static class ImageHelper
//    {
//        public static void LoadImages(HttpFileCollectionBase files, int adId)
//        {
//            if (files != null && files.Count != 0)
//            {
//                for (int i = 0; i < files.Count; i++)
//                {
//                    if (files[i] != null && !string.IsNullOrEmpty(files[i].FileName))
//                    {
//                        string filename = Guid.NewGuid() + Path.GetExtension(files[i].FileName);

//                        LoadImage(files[i], filename, "Ads");

//                        var image = new Image
//                        {
//                            AdId = adId,
//                            LargeImage = @"/Content/Ads/Large/" + filename,
//                            SmallImage = @"/Content/Ads/Small/" + filename,
//                        };

//                        DataBase.UploadImage(image);
//                    }
//                }
//            }
//        }

//        private static void LoadImage(HttpPostedFileBase file, string filename, string folder)
//        {
//            string largePhotoFullName = Path.Combine(HttpContext.Current.Server.MapPath("~/Content"),
//                                                  folder, "Large", file.FileName);
//            string smallPhotoFullName = Path.Combine(HttpContext.Current.Server.MapPath("~/Content"),
//                                                    folder, "Small", file.FileName);

//            if (!String.IsNullOrEmpty(file.FileName))
//            {
//                file.SaveAs(largePhotoFullName);
//                try
//                {
//                    //изменяем размер изображения и перезаписываем на диск
//                    var imageLogo = System.Drawing.Image.FromFile(largePhotoFullName);
//                    var bitmapLogo = new Bitmap(imageLogo);
//                    imageLogo.Dispose();
//                    int newWidth = 500;
//                    bitmapLogo = Resize(bitmapLogo, newWidth, bitmapLogo.Height * newWidth / bitmapLogo.Width);
//                    bitmapLogo.Save(largePhotoFullName);
//                    newWidth = 100;
//                    bitmapLogo = Resize(bitmapLogo, newWidth, bitmapLogo.Height * newWidth / bitmapLogo.Width);
//                    bitmapLogo.Save(smallPhotoFullName);

//                    //пересохраняем с нужным нам названием
//                    File.Move(largePhotoFullName, HttpContext.Current.Server.MapPath("~/Content/" + folder + "/Large/") + filename);
//                    File.Move(smallPhotoFullName, HttpContext.Current.Server.MapPath("~/Content/" + folder + "/Small/") + filename);
//                }
//                catch //пользователь загрузил не изображение (ну или другая ошибка)
//                {
//                    File.Delete(largePhotoFullName);
//                    File.Delete(smallPhotoFullName);
//                }
//            }
//        }

//        private static Bitmap Resize(Bitmap sourceImage, int newWidth, int newHeight)
//        {
//            //меняем размеры логотипа с помощью графики
//            var newLogo = new Bitmap(newWidth, newHeight);
//            var graph = Graphics.FromImage(newLogo);
//            graph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;

//            if (sourceImage.Height > sourceImage.Width)
//            {
//                graph.DrawImage(sourceImage, new Rectangle(0, 0, newWidth, sourceImage.Height * newWidth / sourceImage.Width));
//            }
//            else
//            {
//                graph.DrawImage(sourceImage, new Rectangle(0, 0, sourceImage.Width * newHeight / sourceImage.Height, newHeight));
//            }

//            graph.DrawImageUnscaledAndClipped(newLogo, new Rectangle(0, 0, newWidth, newHeight));

//            return newLogo;
//        }

//        public static void DeleteFile(string path)
//        {
//            if (File.Exists(HttpContext.Current.Server.MapPath("~/") + path))
//            {
//                File.Delete(HttpContext.Current.Server.MapPath("~/") + path);
//            }
//        }
//    }
//}