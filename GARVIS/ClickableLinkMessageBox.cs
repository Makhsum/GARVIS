using System;
using System.Windows.Forms;
using System.Diagnostics;

public class ClickableLinkMessageBox : Form
{
    public ClickableLinkMessageBox(string message, string linkText, string linkUrl)
    {
        InitializeComponent(message, linkText, linkUrl);
    }

    private void InitializeComponent(string message, string linkText, string linkUrl)
    {
        this.Text = "Сообщение";
        this.Size = new System.Drawing.Size(400, 150);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.StartPosition = FormStartPosition.CenterScreen;

        Label label = new Label();
        label.Text = message;
        label.AutoSize = true;
        label.Location = new System.Drawing.Point(20, 20);

        LinkLabel linkLabel = new LinkLabel();
        linkLabel.Text = linkText;
        linkLabel.AutoSize = true;
        linkLabel.Location = new System.Drawing.Point(20, 50);
        linkLabel.Click += (sender, e) => Process.Start(linkUrl);

        this.Controls.Add(label);
        this.Controls.Add(linkLabel);
    }

    public static void Show(string message, string linkText, string linkUrl)
    {
        using (ClickableLinkMessageBox form = new ClickableLinkMessageBox(message, linkText, linkUrl))
        {
            form.ShowDialog();
        }
    }
}
