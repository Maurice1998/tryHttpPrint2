using System;
using System.Drawing;
using System.Drawing.Printing;
using ZXing.Common;

namespace tryHttpPrint2
{
    class Print
    {   
        //Carton Label Print
        public void print1()
        {
            var doc = new PrintDocument();
            doc.PrinterSettings.PrinterName = "Bullzip PDF Printer";
            doc.DocumentName = doc.PrinterSettings.MaximumCopies.ToString();
            doc.PrintPage += new PrintPageEventHandler(this.CartonLabelDocument_PrintPage);
            doc.PrintController = new System.Drawing.Printing.StandardPrintController();
            doc.Print();
            doc.Dispose();

        }


        /// <summary>
        /// 打印的格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CartonLabelDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {/*
            如果需要改变 可以在new Font(new FontFamily("Arial"), 11）中的“Arial”改成自己要的字体就行了，黑体 后面的数字代表字体的大小
             System.Drawing.Brushes.Blue, 170, 10 中的 System.Drawing.Brushes.Blue 为颜色，后面的为输出的位置*/
            EncodingOptions encodeOption = new EncodingOptions();
            encodeOption.Height = 13;
            encodeOption.Width = 200;
            encodeOption.PureBarcode = true;
            ZXing.BarcodeWriter wr = new ZXing.BarcodeWriter();
            wr.Options = encodeOption;
            wr.Format = ZXing.BarcodeFormat.CODE_128;
            var code = "L2014021700000008";
            Bitmap img = wr.Write(code);

            e.Graphics.DrawString($"CartonID: {code}", new Font(new FontFamily("Arial"), 10f, FontStyle.Bold), Brushes.Black, 10, 0);
            e.Graphics.DrawImage(img, 9, 20);
            e.Graphics.DrawString("Print time: " + DateTime.Now.ToString(), new Font(new FontFamily("Arial"), 10f), Brushes.Black, 10, 35);
            e.Graphics.DrawLine(Pens.Black, 10, 50, 220, 50);
            e.Graphics.DrawString("Material:", new Font(new FontFamily("Arial"), 10f, FontStyle.Bold), Brushes.Black, 10, 55);
            e.Graphics.DrawString("Vendor: ", new Font(new FontFamily("Arial"), 10f, FontStyle.Bold), Brushes.Black, 10, 67);
            e.Graphics.DrawString("Quantity: ", new Font(new FontFamily("Arial"), 10f, FontStyle.Bold), Brushes.Black, 10, 79);
            e.Graphics.DrawString("Lot Code: ", new Font(new FontFamily("Arial"), 10f, FontStyle.Bold), Brushes.Black, 10, 91);
            e.Graphics.DrawString("Date Code:", new Font(new FontFamily("Arial"), 10f, FontStyle.Bold), Brushes.Black, 10, 103);
            e.Graphics.DrawString("Batch#: ", new Font(new FontFamily("Arial"), 10f, FontStyle.Bold), Brushes.Black, 10, 115);
            e.Graphics.DrawString("Tooling#: ", new Font(new FontFamily("Arial"), 10f, FontStyle.Bold), Brushes.Black, 10, 127);
            e.Graphics.DrawLine(Pens.Black, 10, 144, 220, 144);
            e.HasMorePages = false;
        }
        /// <summary>
        /// 设置PrintDocument 的相关属性
        /// </summary>
        /// <param name="str">要打印的字符串</param>

        //Pallet Label Print
        public void print2()
        {
            var doc = new PrintDocument();
            //调用的打印机
            doc.PrinterSettings.PrinterName = "Bullzip PDF Printer";
            doc.DocumentName = doc.PrinterSettings.MaximumCopies.ToString();
            doc.PrintPage += new PrintPageEventHandler(this.PalletLabelDocument_PrintPage);
            doc.PrintController = new StandardPrintController();
            doc.Print();
            doc.Dispose();
        }
        /// <summary>
        /// 打印的格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PalletLabelDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {/*
            如果需要改变 可以在new Font(new FontFamily("Arial"), 11）中的“Arial”改成自己要的字体就行了，黑体 后面的数字代表字体的大小
             System.Drawing.Brushes.Blue, 170, 10 中的 System.Drawing.Brushes.Blue 为颜色，后面的为输出的位置*/
            EncodingOptions encodeOption = new EncodingOptions();
            encodeOption.Height = 15;
            encodeOption.Width = 110;
            encodeOption.PureBarcode = true;
            ZXing.BarcodeWriter wr = new ZXing.BarcodeWriter();
            wr.Options = encodeOption;
            wr.Format = ZXing.BarcodeFormat.CODE_128;

            var code = "L2014021700000008";
            string printTime = DateTime.Now.ToString();
            Bitmap img = wr.Write(code);
            Bitmap timeImg = wr.Write(printTime);

            e.Graphics.DrawString("W/H:", new Font(new FontFamily("Arial"), 13f, FontStyle.Bold), Brushes.Black, 50, 5);
            e.Graphics.DrawString("STORE-IN SHEET", new Font(new FontFamily("Arial"), 20f, FontStyle.Bold), Brushes.Black, 260, 2);
            e.Graphics.DrawString("Print time: " + printTime, new Font(new FontFamily("Arial"), 9f), Brushes.Black, 600, 15);
            //打印一串显示时间段 条形码
            e.Graphics.DrawImage(timeImg, 410, 33);
            e.Graphics.DrawString("Pallet No: ", new Font(new FontFamily("Arial"), 13f, FontStyle.Bold), Brushes.Black, 10, 25);
            /*graphics.DrawString(MduLabelPrint.toBarcode(this.m_PalletLabel.LabelID), this.ftCode39, Brushes.Black, (float)intBeginX, (float)checked(intBeginY + num * 2 + 17));*/
            e.Graphics.DrawString("Page: ", new Font(new FontFamily("Arial"), 13f, FontStyle.Bold), Brushes.Black, 660, 27);
            e.Graphics.DrawString("Model: ", new Font(new FontFamily("Arial"), 13f, FontStyle.Bold), Brushes.Black, 10, 47);
            e.Graphics.DrawString("Model Desc: ", new Font(new FontFamily("Arial"), 13f, FontStyle.Bold), Brushes.Black, 305, 47);
            e.Graphics.DrawString("Part No: ", new Font(new FontFamily("Arial"), 13f, FontStyle.Bold), Brushes.Black, 10, 69);
            e.Graphics.DrawString("Qty: ", new Font(new FontFamily("Arial"), 13f, FontStyle.Bold), Brushes.Black, 305, 69);
            /*graphics.DrawString(MduLabelPrint.toBarcode(this.m_PalletLabel.Qty.ToString()), this.ftCode39, Brushes.Black, 120f, (float)checked(intBeginY + num * 5 + 17));*/
            e.Graphics.DrawString("Bin: ", new Font(new FontFamily("Arial"), 13f, FontStyle.Bold), Brushes.Black, 540, 69);
            //双细线画粗线
            e.Graphics.DrawLine(Pens.Black, 265, 30, 500, 30);
            e.Graphics.DrawLine(Pens.Black, 265, 31, 500, 31);
            e.Graphics.DrawLine(Pens.Black, 11, 91, 800, 91);

            e.Graphics.DrawString("MO:", new Font(new FontFamily("Arial"), 10f), Brushes.Black, 10, 92);
            e.Graphics.DrawImage(img, 9, 106);
            e.Graphics.DrawString($"{code}", new Font(new FontFamily("Arial"), 10f), Brushes.Black, 10, 121);
            e.HasMorePages = false;
        }


        //ROH Label Print
        public void print3()
        {
            var doc = new PrintDocument();
            doc.PrinterSettings.PrinterName = "Bullzip PDF Printer";
            doc.DocumentName = doc.PrinterSettings.MaximumCopies.ToString();
            doc.PrintPage += new PrintPageEventHandler(this.ROHLabelDocument_PrintPage);
            doc.PrintController = new System.Drawing.Printing.StandardPrintController();
            doc.Print();
            doc.Dispose();

        }


        /// <summary>
        /// 打印的格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ROHLabelDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {/*
            如果需要改变 可以在new Font(new FontFamily("Arial"), 11）中的“Arial”改成自己要的字体就行了，黑体 后面的数字代表字体的大小
             System.Drawing.Brushes.Blue, 170, 10 中的 System.Drawing.Brushes.Blue 为颜色，后面的为输出的位置*/
            EncodingOptions encodeOption = new EncodingOptions();
            encodeOption.Height = 13;
            encodeOption.Width = 200;
            encodeOption.PureBarcode = true;
            ZXing.BarcodeWriter wr = new ZXing.BarcodeWriter();
            wr.Options = encodeOption;
            wr.Format = ZXing.BarcodeFormat.CODE_128;
            var code = "L2014021700000008";
            Bitmap img = wr.Write(code);

            e.Graphics.DrawString($"Label ID: {code}", new Font(new FontFamily("Arial"), 10f, FontStyle.Bold), Brushes.Black, 10, 0);
            e.Graphics.DrawImage(img, 9, 20);
            e.Graphics.DrawString("Print time: " + DateTime.Now.ToString(), new Font(new FontFamily("Arial"), 7.5f), Brushes.Black, 10, 35);
            e.Graphics.DrawString("K14070391 ", new Font(new FontFamily("Arial"), 7.5f), Brushes.Black, 165, 35);
            e.Graphics.DrawLine(Pens.Black, 10, 50, 245, 50);
            e.Graphics.DrawString("Material:", new Font(new FontFamily("Arial"), 10f), Brushes.Black, 10, 52);
            e.Graphics.DrawString("Bin: ", new Font(new FontFamily("Arial"), 10f), Brushes.Black, 10, 65);
            e.Graphics.DrawString("Vendor: ", new Font(new FontFamily("Arial"), 10f), Brushes.Black, 10, 78);
            e.Graphics.DrawString("GR Date/DC: ", new Font(new FontFamily("Arial"), 10f), Brushes.Black, 10, 91);
            e.Graphics.DrawString("Quantity:", new Font(new FontFamily("Arial"), 10f), Brushes.Black, 10, 103);
            e.Graphics.DrawString("Version/GP: ", new Font(new FontFamily("Arial"), 10), Brushes.Black, 10, 116);
            e.Graphics.DrawString("UP Point: ", new Font(new FontFamily("Arial"), 10f), Brushes.Black, 10, 129);
            e.Graphics.DrawString("Keeper/Pos: ", new Font(new FontFamily("Arial"), 10f), Brushes.Black, 10, 142);
            e.Graphics.DrawLine(Pens.Black, 10, 159, 245, 159);
            e.Graphics.DrawLine(Pens.Black, 100, 50, 100, 159);
            e.HasMorePages = false;
        }

        // F/G Label Print
        public void print4()
        {
            var doc = new PrintDocument();
            doc.PrinterSettings.PrinterName = "Bullzip PDF Printer";
            doc.DocumentName = doc.PrinterSettings.MaximumCopies.ToString();
            doc.PrintPage += new PrintPageEventHandler(this.FGLabelDocument_PrintPage);
            doc.PrintController = new System.Drawing.Printing.StandardPrintController();
            doc.Print();
            doc.Dispose();

        }


        /// <summary>
        /// 打印的格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FGLabelDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {/*
            如果需要改变 可以在new Font(new FontFamily("Arial"), 11）中的“黑体”改成自己要的字体就行了，黑体 后面的数字代表字体的大小
             System.Drawing.Brushes.Blue, 170, 10 中的 System.Drawing.Brushes.Blue 为颜色，后面的为输出的位置*/
            EncodingOptions encodeOption = new EncodingOptions();
            encodeOption.Height = 13;
            encodeOption.Width = 200;
            encodeOption.PureBarcode = true;
            ZXing.BarcodeWriter wr = new ZXing.BarcodeWriter();
            wr.Options = encodeOption;
            wr.Format = ZXing.BarcodeFormat.CODE_128;
            var code = "L2014021700000008";
            Bitmap img = wr.Write(code);

            e.Graphics.DrawString($"Label ID: {code}", new Font(new FontFamily("Arial"), 10f, FontStyle.Bold), Brushes.Black, 10, 0);
            e.Graphics.DrawImage(img, 9, 20);
            e.Graphics.DrawString("Print time: " + DateTime.Now.ToString(), new Font(new FontFamily("Arial"), 7.5f), Brushes.Black, 10, 35);
            e.Graphics.DrawString("K14070391 ", new Font(new FontFamily("Arial"), 7.5f), Brushes.Black, 165, 35);
            e.Graphics.DrawLine(Pens.Black, 10, 50, 245, 50);
            e.Graphics.DrawString("Material:", new Font(new FontFamily("Arial"), 10f), Brushes.Black, 10, 52);
            e.Graphics.DrawString("Bin: ", new Font(new FontFamily("Arial"), 10f), Brushes.Black, 10, 65);
            e.Graphics.DrawString("Vendor: ", new Font(new FontFamily("Arial"), 10f), Brushes.Black, 10, 78);
            e.Graphics.DrawString("GR Date/DC: ", new Font(new FontFamily("Arial"), 10f), Brushes.Black, 10, 91);
            e.Graphics.DrawString("Quantity:", new Font(new FontFamily("Arial"), 10f), Brushes.Black, 10, 103);
            e.Graphics.DrawString("Version/GP: ", new Font(new FontFamily("Arial"), 10), Brushes.Black, 10, 116);
            e.Graphics.DrawString("UP Point: ", new Font(new FontFamily("Arial"), 10f), Brushes.Black, 10, 129);
            e.Graphics.DrawString("Keeper/Pos: ", new Font(new FontFamily("Arial"), 10f), Brushes.Black, 10, 142);
            e.Graphics.DrawLine(Pens.Black, 10, 159, 245, 159);
            e.Graphics.DrawLine(Pens.Black, 100, 50, 100, 159);
            e.HasMorePages = false;
        }
    }
}
