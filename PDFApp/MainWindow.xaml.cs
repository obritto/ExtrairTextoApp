using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.qrcode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PDFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var retangulo = new iTextSharp.text.Rectangle(765, 765);
            var documento = new Document(retangulo);

            var writer = PdfWriter.GetInstance(documento, new FileStream(@"c:\teste\teste.pdf", FileMode.Create));

            documento.Open();

            var imagemDoTopo = iTextSharp.text.Image.GetInstance(@"c:\teste\imagem.png");
            imagemDoTopo.SetAbsolutePosition(0, 100);
            documento.Add(imagemDoTopo);

            PdfContentByte cb = writer.DirectContent;
            BaseFont outraFonte = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, false, false);

            cb.BeginText();
            cb.SetFontAndSize(outraFonte, 12);
            cb.SetColorFill(new BaseColor(51, 51, 51));
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "TESTE", 50, 35, 50);
            cb.EndText();

            var paramQR = new Dictionary<EncodeHintType, object>();
            paramQR.Add(EncodeHintType.CHARACTER_SET, CharacterSetECI.GetCharacterSetECIByName("UTF-8"));
            BarcodeQRCode qrCodigo = new BarcodeQRCode("https://www.youtube.com/channel/UCcOGfx3w8BRxkfSyZFsW5fQ",
                150, 150, paramQR);
            iTextSharp.text.Image imgBarCode = qrCodigo.GetImage();
            imgBarCode.SetAbsolutePosition(200, 200);
            documento.Add(imgBarCode);

            BarcodeEAN codeEAN13 = null;
            codeEAN13 = new BarcodeEAN();
            codeEAN13.CodeType = Barcode.EAN13;
            codeEAN13.ChecksumText = true;
            codeEAN13.GenerateChecksum = true;
            codeEAN13.BarHeight = 12;
            codeEAN13.Code = "1234567890123";
            imgBarCode = codeEAN13.CreateImageWithBarcode(cb, null, null);
            imgBarCode.SetAbsolutePosition(150, 150);
            imgBarCode.Alignment = iTextSharp.text.Image.TEXTWRAP;
            documento.Add(imgBarCode);

            

            documento.Close();

        }
    }
}
