Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing

Public Class MenuAdmin
    Dim listJadwal, listJanji As New List(Of List(Of String))()
    Dim currentPage, totalPage, totalItem As Integer

    Private Sub KelolaJadwalDokterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KelolaJadwalDokterToolStripMenuItem.Click
        Me.Hide()
        KelolaJadwalDokter.Show()
    End Sub

    Private Sub TambahCatatanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TambahCatatanToolStripMenuItem.Click
        Me.Hide()
        TambahCatatan.Show()
    End Sub

    Private Sub LogOutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogOutToolStripMenuItem.Click
        Login.txtUsn.Clear()
        Login.txtPw.Clear()
        Login.Show()
        Me.Hide()
    End Sub

    Private Sub readDataJadwal()
        CMD = New MySqlCommand("select * from tbjadwaldokter", CONN)
        RD = CMD.ExecuteReader
        totalItem = 0
        Do While RD.Read
            Dim dataJadwal As New List(Of String)()
            dataJadwal.Add(RD("idDokter").ToString)
            dataJadwal.Add(RD("namaDokter").ToString)
            dataJadwal.Add(RD("jam").ToString)
            dataJadwal.Add(RD("tanggal").ToString)
            dataJadwal.Add(RD("spesialis").ToString)
            listJadwal.Add(dataJadwal)
            totalItem += 1
        Loop
        totalPage = Math.Ceiling(listJadwal.Count / totalItem)
        RD.Close()
    End Sub

    Private Sub readDataJanji()
        CMD = New MySqlCommand("SELECT j.idJanji, a.nama, d.namaDokter, j.tanggal, j.jam, j.catatan " &
                               "FROM tbjanjikonsul j " &
                               "INNER JOIN tbakun a ON j.idAkun = a.id " &
                               "INNER JOIN tbjadwaldokter d ON j.idDokter = d.idDokter", CONN)
        RD = CMD.ExecuteReader
        totalItem = 0
        Do While RD.Read
            Dim dataJanji As New List(Of String)()
            dataJanji.Add(RD("idJanji").ToString)
            dataJanji.Add(RD("nama").ToString)
            dataJanji.Add(RD("namaDokter").ToString)
            dataJanji.Add(RD("tanggal").ToString)
            dataJanji.Add(RD("jam").ToString)
            dataJanji.Add(RD("catatan").ToString)
            listJanji.Add(dataJanji)
            totalItem += 1
        Loop
        totalPage = Math.Ceiling(listJanji.Count / totalItem)
        RD.Close()
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim Fheader As New Font("Times New Roman", 24, FontStyle.Bold)
        Dim FBodyB As New Font("Times New Roman", 14, FontStyle.Bold)
        Dim FBody As New Font("Times New Roman", 14, FontStyle.Regular)

        Dim black As SolidBrush = New SolidBrush(Color.Black)
        Dim center As New StringFormat()
        center.Alignment = StringAlignment.Center

        Dim posY As Integer = e.MarginBounds.Top
        Dim x As Integer = e.MarginBounds.Left
        Dim marginRight As Integer = e.MarginBounds.Right

        e.Graphics.DrawString("Jadwal Dokter", Fheader, black, e.MarginBounds.Width / 2, posY, center)
        posY += 70

        e.Graphics.DrawLine(Pens.Black, x, posY, marginRight, posY)
        posY += 10 ' Add some space after the line

        Dim itemsPerPage As Integer = 5
        Dim startIndex As Integer = (currentPage - 1) * itemsPerPage
        Dim endIndex As Integer = Math.Min(startIndex + itemsPerPage, listJadwal.Count)

        For i As Integer = startIndex To endIndex - 1
            e.Graphics.DrawString("ID Dokter", FBodyB, black, x + 20, posY)
            e.Graphics.DrawString(": " & listJadwal(i)(0), FBody, black, x + 200, posY)
            posY += 30

            e.Graphics.DrawString("Nama Dokter", FBodyB, black, x + 20, posY)
            e.Graphics.DrawString(": " & listJadwal(i)(1), FBody, black, x + 200, posY)
            posY += 30

            e.Graphics.DrawString("Jam", FBodyB, black, x + 20, posY)
            e.Graphics.DrawString(": " & listJadwal(i)(2), FBody, black, x + 200, posY)
            posY += 30

            Dim tanggalFormatted As String = DateTime.Parse(listJadwal(i)(3)).ToString("yyyy-MM-dd")
            e.Graphics.DrawString("Tanggal", FBodyB, black, x + 20, posY)
            e.Graphics.DrawString(": " & tanggalFormatted, FBody, black, x + 200, posY)
            posY += 30

            e.Graphics.DrawString("Spesialis", FBodyB, black, x + 20, posY)
            e.Graphics.DrawString(": " & listJadwal(i)(4), FBody, black, x + 200, posY)
            posY += 40

            e.Graphics.DrawLine(Pens.Black, x, posY, marginRight, posY)
            posY += 10
        Next
        currentPage += 1
        e.HasMorePages = endIndex < listJadwal.Count

        If Not e.HasMorePages Then
            currentPage = 1
        End If
    End Sub

    Private Sub PrintDocument2_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument2.PrintPage
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

        e.Graphics.DrawString("Jadwal Konsultasi", Fheader, black, pageWidth / 2, posY, center)
        posY += 70

        e.Graphics.DrawLine(Pens.Black, x, posY, marginRight, posY)
        posY += 10

        Dim itemsPerPage As Integer = 4
        Dim startIndex As Integer = (currentPage - 1) * itemsPerPage
        Dim endIndex As Integer = Math.Min(startIndex + itemsPerPage, listJanji.Count)

        For i As Integer = startIndex To endIndex - 1
            If i >= listJanji.Count Then Exit For

            e.Graphics.DrawString("ID Janji", FBodyB, black, x + 20, posY)
            e.Graphics.DrawString(": " & listJanji(i)(0), FBody, black, x + 200, posY)
            posY += 30

            e.Graphics.DrawString("Nama Pasien", FBodyB, black, x + 20, posY)
            e.Graphics.DrawString(": " & listJanji(i)(1), FBody, black, x + 200, posY)
            posY += 30

            e.Graphics.DrawString("Nama Dokter", FBodyB, black, x + 20, posY)
            e.Graphics.DrawString(": " & listJanji(i)(2), FBody, black, x + 200, posY)
            posY += 30

            e.Graphics.DrawString("Tanggal", FBodyB, black, x + 20, posY)
            Dim tanggalFormatted As String = DateTime.Parse(listJanji(i)(3)).ToString("yyyy-MM-dd")
            e.Graphics.DrawString(": " & tanggalFormatted, FBody, black, x + 200, posY)
            posY += 30

            e.Graphics.DrawString("Jam", FBodyB, black, x + 20, posY)
            e.Graphics.DrawString(": " & listJanji(i)(4), FBody, black, x + 200, posY)
            posY += 30

            e.Graphics.DrawString("Catatan", FBodyB, black, x + 20, posY)
            Dim catatanText As String = If(String.IsNullOrEmpty(listJanji(i)(5)), "Catatan belum tersedia", listJanji(i)(5))
            e.Graphics.DrawString(": " & catatanText, FBody, black, x + 200, posY)
            posY += 40

            e.Graphics.DrawLine(Pens.Black, x, posY, marginRight, posY)
            posY += 10
        Next

        currentPage += 1
        e.HasMorePages = endIndex < listJanji.Count

        If Not e.HasMorePages Then
            currentPage = 1
        End If
    End Sub

    Private Sub LaporanJadwalDokterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanJadwalDokterToolStripMenuItem.Click
        readDataJadwal()
        currentPage = 1
        PrintPreviewDialog1.Document = PrintDocument1
        PrintPreviewDialog1.ShowDialog()
        Dim result As DialogResult = MessageBox.Show("Apakah Anda ingin mencetak catatan?", "Konfirmasi Cetak", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            PrintDocument1.Print()
        Else
            Me.Show()
        End If
    End Sub

    Private Sub LaporanKonsultasiPasienToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanKonsultasiPasienToolStripMenuItem.Click
        readDataJanji()
        currentPage = 1
        PrintPreviewDialog1.Document = PrintDocument2
        PrintPreviewDialog1.ShowDialog()
        Dim result As DialogResult = MessageBox.Show("Apakah Anda ingin mencetak catatan?", "Konfirmasi Cetak", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            PrintDocument2.Print()
        Else
            Me.Show()
        End If
    End Sub

    Private Sub MenuAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
    End Sub
End Class


