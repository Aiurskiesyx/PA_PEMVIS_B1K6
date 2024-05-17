Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing

Public Class MenuPasien
    Private AllCatatan As New List(Of String())
    Private currentPage As Integer = 1
    Private totalPage As Integer
    Private itemsPerPage As Integer = 5

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button1.BackColor = Color.FromArgb(134, 167, 137)
        Button2.BackColor = Color.FromArgb(134, 167, 137)
        Button3.BackColor = Color.FromArgb(134, 167, 137)
    End Sub

    Private Sub Button_MouseEnter(sender As Object, e As EventArgs) Handles Button1.MouseEnter, Button2.MouseEnter, Button3.MouseEnter
        Dim button As Button = DirectCast(sender, Button)
        button.BackColor = Color.FromArgb(235, 243, 232)
    End Sub

    Private Sub Button_MouseLeave(sender As Object, e As EventArgs) Handles Button1.MouseLeave, Button2.MouseLeave, Button3.MouseLeave
        Dim button As Button = DirectCast(sender, Button)
        button.BackColor = Color.FromArgb(134, 167, 137)
    End Sub

    Private Sub Button_Click(sender As Object, e As EventArgs) Handles Button1.Click, Button2.Click, Button3.Click
        Dim button As Button = DirectCast(sender, Button)

        Select Case button.Name
            Case "Button1"
                ' Tindakan untuk Button1

            Case "Button2"
                Me.Hide()
                JanjiTemuPasien.Show()

            Case "Button3"
                TampilCatatan()
                If AllCatatan.Count = 0 Then
                    MessageBox.Show("Tidak ada catatan yang tersedia untuk dicetak.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                totalPage = Math.Ceiling(AllCatatan.Count / itemsPerPage)
                currentPage = 1
                PrintPreviewDialog1.Document = PrintDocument1
                PrintPreviewDialog1.ShowDialog()

                Dim result As DialogResult = MessageBox.Show("Apakah Anda ingin mencetak catatan?", "Konfirmasi Cetak", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    PrintDocument1.Print()
                Else
                    Me.Show()
                End If
        End Select
    End Sub

    Private Sub btnKeluar_Click(sender As Object, e As EventArgs) Handles btnKeluar.Click
        Login.txtUsn.Clear()
        Login.txtPw.Clear()
        Login.Show()
        Me.Hide()
    End Sub

    Sub TampilCatatan()
        koneksi()
        Dim idAkun As Integer = Login.globalId
        Dim query As String = "SELECT tbjadwaldokter.namaDokter, tbjanjikonsul.tanggal, tbjanjikonsul.jam, tbjanjikonsul.catatan " &
                              "FROM tbjanjikonsul " &
                              "INNER JOIN tbjadwaldokter ON tbjanjikonsul.idDokter = tbjadwaldokter.idDokter " &
                              "WHERE tbjanjikonsul.idAkun = @idAkun"
        Using cmd As New MySqlCommand(query, CONN)
            cmd.Parameters.AddWithValue("@idAkun", idAkun)
            Dim da As New MySqlDataAdapter(cmd)
            Dim ds As New DataSet
            da.Fill(ds, "tbjanjikonsul")

            AllCatatan.Clear()
            For Each row As DataRow In ds.Tables("tbjanjikonsul").Rows
                AllCatatan.Add(New String() {row("namaDokter").ToString(), row("tanggal").ToString(), row("jam").ToString(), row("catatan").ToString()})
            Next
        End Using
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        koneksi()
        Dim Fheader As New Font("Times New Roman", 24, FontStyle.Bold)
        Dim FBodyB As New Font("Times New Roman", 14, FontStyle.Bold)
        Dim FBody As New Font("Times New Roman", 14, FontStyle.Regular)
        Dim black As SolidBrush = New SolidBrush(Color.Black)
        Dim center As New StringFormat()
        center.Alignment = StringAlignment.Center

        Dim posY As Integer = e.MarginBounds.Top
        Dim x As Integer = e.MarginBounds.Left
        Dim marginRight As Integer = e.MarginBounds.Right
        Dim pageWidth As Integer = e.MarginBounds.Width

        e.Graphics.DrawString("Janji Konsultasi", Fheader, black, pageWidth / 2, posY, center)
        posY += 70

        e.Graphics.DrawLine(Pens.Black, x, posY, marginRight, posY)

        For i As Integer = (currentPage - 1) * itemsPerPage To AllCatatan.Count - 1
            If i >= AllCatatan.Count Then Exit For

            e.Graphics.DrawString("Nama Dokter", FBodyB, black, x + 20, posY + 30)
            e.Graphics.DrawString(": " & AllCatatan(i)(0), FBody, black, x + 200, posY + 30)
            e.Graphics.DrawString("Tanggal", FBodyB, black, x + 20, posY + 60)
            e.Graphics.DrawString(": " & AllCatatan(i)(1), FBody, black, x + 200, posY + 60)
            e.Graphics.DrawString("Jam", FBodyB, black, x + 20, posY + 90)
            e.Graphics.DrawString(": " & AllCatatan(i)(2), FBody, black, x + 200, posY + 90)
            e.Graphics.DrawString("Catatan", FBodyB, black, x + 20, posY + 120)

            Dim catatanText As String = If(String.IsNullOrEmpty(AllCatatan(i)(3)), "Catatan belum tersedia", AllCatatan(i)(3))
            e.Graphics.DrawString(": " & catatanText, FBody, black, x + 200, posY + 120)

            e.Graphics.DrawLine(Pens.Black, x, posY + 170, marginRight, posY + 170)
            posY += 170

            If (i + 1) Mod itemsPerPage = 0 Then Exit For
        Next

        e.Graphics.DrawLine(Pens.Black, x, posY, marginRight, posY)

        currentPage += 1
        e.HasMorePages = currentPage <= totalPage

        If Not e.HasMorePages Then
            currentPage = 1
        End If
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub
End Class