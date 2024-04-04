using System;
using System.Drawing;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // This loads the image from the file path
            Bitmap image = LoadImage(@"D:\kratos.jpg");
            
            //Play around with this to see the desired output 
            int outputWidth = 140;

            // this one converts the image to the ascii art and prints it to the console
            string asciiArt = ConvertToAscii(image, outputWidth);
            Console.WriteLine(asciiArt);
        }
        catch (Exception ex)
        {
            // This one handles any exceptions that can happen during the image loading or during the conversion
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
        
    // Method to load an image from a file
    static Bitmap LoadImage(string filePath)
    {
        // Checks if the file exists at the specified path
        if (!System.IO.File.Exists(filePath))
            throw new ArgumentException("File does not exist.");


        // Load the image from the file and returns it
        return new Bitmap(filePath);
    }

    // Method to convert an image to ASCII art
    static string ConvertToAscii(Bitmap image, int outputWidth)
    {
        char[] asciiChars = [' ', '.', ':', '-', '=', '+', '*', '#', '%', '@'];
        int imageWidth = image.Width;
        int imageHeight = image.Height;
        
        //Calculates the height of the ASCII art based on the output width
        int asciiHeight = (int)(imageHeight * (outputWidth / (double)imageWidth));

        //Resizes the image to the desired output width and ASCII height
        Bitmap resizedImage = new Bitmap(image, outputWidth, asciiHeight);

        // Itreate over each row of the resized image
        string asciiArt = "";

        for (int y = 0; y < asciiHeight; y++)
        {
            //Iterate over each pixel in the current row
            for (int x = 0; x < outputWidth; x++)
            {

                //Gets the color of the current pixel
                Color pixelColor = resizedImage.GetPixel(x, y);

                // Calculate the intensity of the pixel (average of RGB values)
                int intensity = (int)((pixelColor.R + pixelColor.G + pixelColor.B) / 3.0);

                // Map the intensity to an ASCII character based on the predefined character set
                int index = intensity * (asciiChars.Length - 1) / 255;
                asciiArt += asciiChars[index];
            }
            // Add a newline character after processing each row of pixels
            asciiArt += "\n";
        }

        return asciiArt;
    }
}
