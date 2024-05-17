<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class JanjiTemuPasien
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cbSpesialis = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cbTanggal = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbJam = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnSimpan = New System.Windows.Forms.Button()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.cbDokter = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.username = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Panel2.Controls.Add(Me.cbSpesialis)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.cbTanggal)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.cbJam)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.btnSimpan)
        Me.Panel2.Controls.Add(Me.btnReset)
        Me.Panel2.Controls.Add(Me.cbDokter)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Location = New System.Drawing.Point(358, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(796, 702)
        Me.Panel2.TabIndex = 3
        '
        'cbSpesialis
        '
        Me.cbSpesialis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSpesialis.Font = New System.Drawing.Font("Rockwell", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSpesialis.FormattingEnabled = True
        Me.cbSpesialis.Items.AddRange(New Object() {"Umum", "Gigi", "Penyakit Dalam", "Bedah Umum", "Mata", "Jiwa"})
        Me.cbSpesialis.Location = New System.Drawing.Point(225, 256)
        Me.cbSpesialis.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbSpesialis.Name = "cbSpesialis"
        Me.cbSpesialis.Size = New System.Drawing.Size(475, 30)
        Me.cbSpesialis.TabIndex = 26
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Rockwell Condensed", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(37, 256)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(151, 29)
        Me.Label6.TabIndex = 25
        Me.Label6.Text = "Pilih Spesialis"
        '
        'cbTanggal
        '
        Me.cbTanggal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTanggal.Font = New System.Drawing.Font("Rockwell", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbTanggal.FormattingEnabled = True
        Me.cbTanggal.Location = New System.Drawing.Point(225, 414)
        Me.cbTanggal.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbTanggal.Name = "cbTanggal"
        Me.cbTanggal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cbTanggal.Size = New System.Drawing.Size(475, 30)
        Me.cbTanggal.TabIndex = 24
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Rockwell", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(200, 84)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(407, 44)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "KLINIK PRIMA CARE"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Rockwell", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(318, 145)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(198, 29)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Janji Konsultasi"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Rockwell Condensed", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(37, 419)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(149, 29)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "Pilih Tanggal"
        '
        'cbJam
        '
        Me.cbJam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbJam.Font = New System.Drawing.Font("Rockwell", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbJam.FormattingEnabled = True
        Me.cbJam.Location = New System.Drawing.Point(225, 499)
        Me.cbJam.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbJam.Name = "cbJam"
        Me.cbJam.Size = New System.Drawing.Size(475, 30)
        Me.cbJam.TabIndex = 18
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Rockwell Condensed", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(39, 499)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(107, 29)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "Pilih Jam"
        '
        'btnSimpan
        '
        Me.btnSimpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(178, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(186, Byte), Integer))
        Me.btnSimpan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSimpan.Font = New System.Drawing.Font("Rockwell", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSimpan.Location = New System.Drawing.Point(302, 574)
        Me.btnSimpan.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnSimpan.Name = "btnSimpan"
        Me.btnSimpan.Size = New System.Drawing.Size(129, 55)
        Me.btnSimpan.TabIndex = 16
        Me.btnSimpan.Text = "Simpan"
        Me.btnSimpan.UseVisualStyleBackColor = False
        '
        'btnReset
        '
        Me.btnReset.BackColor = System.Drawing.Color.FromArgb(CType(CType(178, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(186, Byte), Integer))
        Me.btnReset.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnReset.Font = New System.Drawing.Font("Rockwell", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(467, 574)
        Me.btnReset.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(129, 55)
        Me.btnReset.TabIndex = 14
        Me.btnReset.Text = "Reset"
        Me.btnReset.UseVisualStyleBackColor = False
        '
        'cbDokter
        '
        Me.cbDokter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbDokter.Font = New System.Drawing.Font("Rockwell", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbDokter.FormattingEnabled = True
        Me.cbDokter.Location = New System.Drawing.Point(225, 332)
        Me.cbDokter.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbDokter.Name = "cbDokter"
        Me.cbDokter.Size = New System.Drawing.Size(475, 30)
        Me.cbDokter.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Rockwell Condensed", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(37, 338)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(128, 29)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Pilih Dokter"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(134, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(137, Byte), Integer))
        Me.Panel1.BackgroundImage = Global.designPA.My.Resources.Resources.pngtree_cute_nurse_cartoon_illustration_png_image_5358292_removebg_preview
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Panel1.Controls.Add(Me.username)
        Me.Panel1.Controls.Add(Me.btnBack)
        Me.Panel1.Controls.Add(Me.Button6)
        Me.Panel1.Location = New System.Drawing.Point(-2, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(362, 702)
        Me.Panel1.TabIndex = 2
        '
        'username
        '
        Me.username.AutoSize = True
        Me.username.Font = New System.Drawing.Font("Rockwell", 13.875!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.username.Location = New System.Drawing.Point(93, 549)
        Me.username.Name = "username"
        Me.username.Size = New System.Drawing.Size(178, 33)
        Me.username.TabIndex = 4
        Me.username.Text = "USERNAME"
        '
        'btnBack
        '
        Me.btnBack.BackgroundImage = Global.designPA.My.Resources.Resources.icons8_back_48
        Me.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.Location = New System.Drawing.Point(16, 608)
        Me.btnBack.Margin = New System.Windows.Forms.Padding(2)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(57, 54)
        Me.btnBack.TabIndex = 7
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.Color.Transparent
        Me.Button6.BackgroundImage = Global.designPA.My.Resources.Resources.icons8_test_account_48
        Me.Button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button6.FlatAppearance.BorderSize = 0
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button6.Location = New System.Drawing.Point(118, 30)
        Me.Button6.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(118, 125)
        Me.Button6.TabIndex = 6
        Me.Button6.UseVisualStyleBackColor = False
        '
        'JanjiTemuPasien
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1154, 696)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "JanjiTemuPasien"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "JanjiTemuPasien"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents cbDokter As System.Windows.Forms.ComboBox
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents btnSimpan As System.Windows.Forms.Button
    Friend WithEvents cbJam As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents cbTanggal As System.Windows.Forms.ComboBox
    Friend WithEvents cbSpesialis As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents username As System.Windows.Forms.Label
End Class
