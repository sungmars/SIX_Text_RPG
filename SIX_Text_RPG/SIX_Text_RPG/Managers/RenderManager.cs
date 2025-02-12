using OpenCvSharp;

namespace SIX_Text_RPG.Managers
{
    internal class RenderManager
    {
        public static RenderManager Instance { get; private set; } = new();

        public void Render(string fileName)
        {
            string filePath = $"Video/{fileName}.mp4";
            using var capturer = new VideoCapture(filePath);

            Mat frame = new();
            while (true)
            {
                capturer.Read(frame);
                if (frame.Empty()) break; // 비디오 끝

                string asciiArt = ConvertToAscii(frame);
            }
        }

        private string ConvertToAscii(Mat image)
        {
            // ASCII 문자 목록 (밝기 순서대로)
            string chars = "@%#*+=-:. ";

            // 해상도 축소
            int width = 80;
            int height = (int)(image.Rows / (image.Cols / (double)width) / 2);
            Mat resized = new();
            Cv2.Resize(image, resized, new(width, height));

            // 그레이스케일 변환
            Mat gray = new();
            Cv2.CvtColor(resized, gray, ColorConversionCodes.BGR2GRAY);

            // ASCII 변환
            char[] ascii = new char[width * height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte pixel = gray.At<byte>(x, y);
                    int index = pixel * (chars.Length - 1) / 255;
                    ascii[y * width + x] = chars[index];
                }
            }

            // 줄바꿈 추가
            return string.Join("\n", Enumerable.Range(0, height).Select(y => new string(ascii, y * width, width)));
        }
    }
}