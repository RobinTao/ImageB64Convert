using System.Drawing.Imaging;

namespace ImageB64Convert
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
        }

        private void LoadImage()
        {
            if (!Clipboard.ContainsImage())
            {
                return;
            }

            Size sizeDiff = this.Size - pictureBox1.Size;
            pictureBox1.Image = Clipboard.GetImage();
            this.Size = pictureBox1.Size + sizeDiff;
        }

        private void buttonCopyB64_Click(object sender, EventArgs e)
        {
            Image image = pictureBox1.Image;
            if (image == null)
            {
                return;
            }

            // [doge]:data:image/png;base64,iVBORw0......
            string b64str = ToBase64String(image);
            Clipboard.SetText($"![][img]\n\r[img]:data:image/png;base64,{b64str}");
        }

        private static string ToBase64String(Image image)
        {
            using (MemoryStream m = new MemoryStream())
            {
                //image.Save(m, image.RawFormat);
                image.Save(m, ImageFormat.Png);
                byte[] imageBytes = m.ToArray();

                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        private void buttonPaste_Click(object sender, EventArgs e)
        {
            LoadImage();
        }

        private void FormMain_Activated(object sender, EventArgs e)
        {
            LoadImage();
        }
    }
}