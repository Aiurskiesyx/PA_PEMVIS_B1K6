﻿Imports MySql.Data.MySqlClient

Public Class KelolaJadwalDokter
    Dim UM As Integer = 0
    Dim GG As Integer = 0
    Dim PDL As Integer = 0
    Dim BDU As Integer = 0
    Dim MTA As Integer = 0
    Dim JWA As Integer = 0
    Dim Gelar As String = ""
    Sub Kosong()
        txtKode.Clear()
        txtNamaDokter.Clear()
        cbSpesialis.SelectedIndex = -1
        DateTimePicker1.Value = DateTime.Now
        For Each cb As CheckBox In GroupBox1.Controls.OfType(Of CheckBox)()
            cb.Checked = False
        Next
        cbSpesialis.Focus()
    End Sub

    Sub tampilDokter()
        DataGridView1.Rows.Clear()
        CMD = New MySqlCommand("select * from tbjadwaldokter", CONN)
        RD = CMD.ExecuteReader()
        While RD.Read()
            Dim row As New DataGridViewRow()
            row.CreateCells(DataGridView1)
            row.Cells(0).Value = RD("idDokter")
            row.Cells(1).Value = Gelar & " " & RD("namaDokter") ' Tambahkan gelar di depan nama dokter
            row.Cells(2).Value = RD("spesialis")
            row.Cells(3).Value = RD("tanggal")
            row.Cells(4).Value = RD("jam")
            DataGridView1.Rows.Add(row)
        End While
        RD.Close()
    End Sub

    Sub aturGrid()
        DataGridView1.Columns(0).Width = 10
        DataGridView1.Columns(1).Width = 100
        DataGridView1.Columns(2).Width = 100
        DataGridView1.Columns(3).Width = 100
        DataGridView1.Columns(4).Width = 100
        DataGridView1.Columns(0).HeaderText = "Kode"
        DataGridView1.Columns(1).HeaderText = "Nama Dokter"
        DataGridView1.Columns(2).HeaderText = "Spesialis"
        DataGridView1.Columns(3).HeaderText = "Tanggal"
        DataGridView1.Columns(4).HeaderText = "Jam"
    End Sub
    Private Sub KelolaJadwalDokter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
        tampilDokter()
        Kosong()
        DateTimePicker1.MinDate = DateTime.Today
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells("Spesialis").Value IsNot Nothing AndAlso
                row.Cells("Spesialis").Value.ToString() = "Umum" Then
                UM += 1
            ElseIf row.Cells("Spesialis").Value IsNot Nothing AndAlso
                row.Cells("Spesialis").Value.ToString() = "Gigi" Then
                GG += 1
            ElseIf row.Cells("Spesialis").Value IsNot Nothing AndAlso
                row.Cells("Spesialis").Value.ToString() = "Penyakit Dalam" Then
                PDL += 1
            ElseIf row.Cells("Spesialis").Value IsNot Nothing AndAlso
                row.Cells("Spesialis").Value.ToString() = "Bedah Umum" Then
                BDU += 1
            ElseIf row.Cells("Spesialis").Value IsNot Nothing AndAlso
                row.Cells("Spesialis").Value.ToString() = "Mata" Then
                MTA += 1
            ElseIf row.Cells("Spesialis").Value IsNot Nothing AndAlso
                row.Cells("Spesialis").Value.ToString() = "Jiwa" Then
                JWA += 1
            End If
        Next
    End Sub
    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click

        ' Validasi input yang diperlukan
        If txtKode.Text = "" Or txtNamaDokter.Text = "" Or cbSpesialis.SelectedItem Is Nothing Then
            MsgBox("Data Belum Lengkap")
            txtKode.Focus()
            Return
        End If

        ' Validasi tanggal yang dipilih
        If DateTimePicker1.Value.DayOfWeek = DayOfWeek.Saturday Or DateTimePicker1.Value.DayOfWeek = DayOfWeek.Sunday Then
            MsgBox("Mohon pilih tanggal pada hari Senin hingga Jumat.", MsgBoxStyle.Exclamation, "Tanggal Tidak Valid")
            Return
        End If

        ' Memformat jam yang dipilih
        Dim jam As String = ""
        For Each cb As CheckBox In GroupBox1.Controls.OfType(Of CheckBox)()
            If cb.Checked Then
                jam &= cb.Text & ", "
            End If
        Next
        If jam.Length > 0 Then
            jam = jam.Substring(0, jam.Length - 2)
        End If

        ' Menyimpan data ke database
        Dim query As String = "INSERT INTO tbjadwaldokter (idDokter, namaDokter, spesialis, tanggal, jam) " &
                              "VALUES (@idDokter, @namaDokter, @spesialis, @tanggal, @jam)"

        Using connection As New MySqlConnection("server=localhost;userid=root;password=;database=dbklink")
            connection.Open()
            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@idDokter", txtKode.Text)
                cmd.Parameters.AddWithValue("@namaDokter", GetFormattedNamaDokter(cbSpesialis.SelectedItem.ToString(), txtNamaDokter.Text))
                cmd.Parameters.AddWithValue("@spesialis", cbSpesialis.SelectedItem.ToString())
                cmd.Parameters.AddWithValue("@tanggal", DateTimePicker1.Value.ToString("yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("@jam", jam)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        ' Menampilkan data terbaru di DataGridView
        tampilDokter()
        Kosong()
        MsgBox("Simpan data sukses!")
    End Sub

    ' Fungsi untuk mendapatkan nama dokter yang diformat dengan gelar sesuai spesialis
    Private Function GetFormattedNamaDokter(ByVal spesialis As String, ByVal namaDokter As String) As String
        Select Case spesialis
            Case "Umum"
                Return "dr. " & namaDokter
            Case "Gigi"
                Return "drg. " & namaDokter
            Case "Penyakit Dalam"
                Return namaDokter & " Sp.PD"
            Case "Bedah Umum"
                Return namaDokter & " Sp.B"
            Case "Mata"
                Return namaDokter & " Sp.M"
            Case "Jiwa"
                Return namaDokter & " Sp.Kj"
            Case Else
                Return namaDokter ' Fallback jika spesialis tidak dikenali
        End Select
    End Function


    Private Function IsDuplicateKodeDokter(ByVal kodeDokter As String) As Boolean
        ' Fungsi untuk memeriksa apakah kode dokter sudah ada dalam database
        Dim query As String = "SELECT COUNT(*) FROM tbjadwaldokter WHERE idDokter = @idDokter"
        Using connection As New MySqlConnection("server=localhost;userid=root;password=;database=dbklink")
            connection.Open()
            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@idDokter", kodeDokter)
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return count > 0
            End Using
        End Using
    End Function


    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        ' Pastikan baris yang dipilih valid (bukan baris header atau baris kosong)
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            ' Dapatkan baris yang dipilih
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            ' Isi elemen formulir dengan data dari baris yang dipilih
            txtKode.Text = row.Cells(0).Value.ToString()
            txtNamaDokter.Text = row.Cells(1).Value.ToString()
            ' Tentukan tanggal
            Dim tanggal As Date = DateTime.Parse(row.Cells(3).Value.ToString())
            'DateTimePicker1.Value = tanggal

            ' Tentukan jam
            Dim jam As String = row.Cells(4).Value.ToString()
            CheckBox1.Checked = jam.Contains("08.00-08.45")
            CheckBox2.Checked = jam.Contains("09.00-09.45")
            CheckBox3.Checked = jam.Contains("10.00-10.45")
            CheckBox4.Checked = jam.Contains("11.00-12.00")
            CheckBox5.Checked = jam.Contains("14.00-14.45")
            CheckBox6.Checked = jam.Contains("15.00-15.45")
            CheckBox7.Checked = jam.Contains("16.00-17.00")
            CheckBox8.Checked = jam.Contains("18.00-18.45")
            CheckBox9.Checked = jam.Contains("19.00-19.45")
            CheckBox10.Checked = jam.Contains("20.00-20.45")
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim tanggal As Date = DateTimePicker1.Value
        Dim formatTanggalWaktu As String = tanggal.ToString("yyyy-MM-dd")
        If txtKode.Text = "" Then
            MsgBox("Kode Dokter Kosong", MsgBoxStyle.Exclamation, "Peringatan")
            Return
        End If

        ' Validasi hari kerja (Senin-Jumat)
        If DateTimePicker1.Value.DayOfWeek = DayOfWeek.Saturday Or DateTimePicker1.Value.DayOfWeek = DayOfWeek.Sunday Then
            MsgBox("Pembaruan data hanya dapat dilakukan pada hari Senin hingga Jumat.", MsgBoxStyle.Exclamation, "Peringatan")
            Return
        End If

        ' Memformat jam yang dipilih
        Dim jam As String = ""
        For Each cb As CheckBox In GroupBox1.Controls.OfType(Of CheckBox)()
            If cb.Checked Then
                jam &= cb.Text & ", "
            End If
        Next
        If jam.Length > 0 Then
            jam = jam.Substring(0, jam.Length - 2)
        End If

        ' Memperbarui data di database
        Dim query As String = "UPDATE tbjadwaldokter SET tanggal = '" & formatTanggalWaktu & "', jam = @jam WHERE idDokter = @idDokter"

        Dim connectionString As String = "server=localhost;userid=root;password=;database=dbklink"

        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            If connection.State = ConnectionState.Open Then
                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@tanggal", DateTimePicker1.Value.ToString("yyyy-MM-dd"))
                    cmd.Parameters.AddWithValue("@jam", jam)
                    cmd.Parameters.AddWithValue("@idDokter", txtKode.Text)
                    cmd.ExecuteNonQuery()
                End Using
                ' Menampilkan data terbaru di DataGridView
                tampilDokter()
                Kosong()
                MsgBox("Data berhasil diubah!")
            Else
                MsgBox("Failed to open the database connection.", MsgBoxStyle.Exclamation, "Error")
            End If
        End Using
    End Sub


    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        If TextBox3.Text <> Nothing Then
            CMD = New MySqlCommand("select * from tbjadwaldokter where idDokter like '%" & TextBox3.Text & "%'", CONN)
            RD = CMD.ExecuteReader()
            If RD.HasRows Then
                DataGridView1.Rows.Clear()
                While RD.Read()
                    Dim row As New DataGridViewRow()
                    row.CreateCells(DataGridView1)
                    row.Cells(0).Value = RD("idDokter")
                    row.Cells(1).Value = RD("namaDokter")
                    row.Cells(2).Value = RD("spesialis")
                    row.Cells(3).Value = RD("tanggal")
                    row.Cells(4).Value = RD("jam")
                    DataGridView1.Rows.Add(row)
                End While
            Else
                MsgBox("Data tidak ditemukan")
            End If
            RD.Close()
        Else
            tampilDokter()
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Kosong()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Hide()
        MenuAdmin.Show()
    End Sub
    Public Sub HanyaHuruf(e As KeyPressEventArgs)
        Dim tombol As Integer = Asc(e.KeyChar)
        If Not (((tombol >= 65) And (tombol <= 90)) Or ((tombol >= 97) And
       (tombol <= 122)) Or (tombol = 8) Or (tombol = 32) Or (tombol = 46)) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtNamaDokter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNamaDokter.KeyPress
        HanyaHuruf(e)
    End Sub
    Public Sub HanyaAngka(e As KeyPressEventArgs)
        Dim tombol As Integer = Asc(e.KeyChar)
        If Not (((tombol >= 48) And (tombol <= 57)) Or (tombol = 8)) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKode.KeyPress
        HanyaAngka(e)
    End Sub

    Private Sub cbSpesialis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSpesialis.SelectedIndexChanged
        If cbSpesialis.Text = "Umum" Then
            txtKode.Text = "U" & UM
        ElseIf cbSpesialis.Text = "Gigi" Then
            txtKode.Text = "G" & GG
        ElseIf cbSpesialis.Text = "Penyakit Dalam" Then
            txtKode.Text = "PD" & PDL
        ElseIf cbSpesialis.Text = "Bedah Umum" Then
            txtKode.Text = "BU" & BDU
        ElseIf cbSpesialis.Text = "Mata" Then
            txtKode.Text = "MT" & MTA
        ElseIf cbSpesialis.Text = "Jiwa" Then
            txtKode.Text = "JW" & JWA
        End If
        Call tampilDokter()
    End Sub

    'Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
    '    If txtKode.Text = "" Then
    '        MessageBox.Show("ID Belum Diisi", "PERINGATAN", MessageBoxButtons.OK,
    '                        MessageBoxIcon.Error)
    '        txtKode.Focus()
    '    Else
    '        If MessageBox.Show("Yakin akan menghapus jadwal dengan kode = " & txtKode.Text &
    '                           " ?", "PERHATIAN!", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
    '            CMD = New MySqlCommand("Delete From tbjadwaldokter where idDokter = '" & txtKode.Text & "'", CONN)
    '            CMD.ExecuteNonQuery()
    '            txtKode.Clear()
    '            Call tampilDokter()
    '            MsgBox("Jadwal Telah Dihapus!", MsgBoxStyle.Information, "PERHATIAN!")
    '        Else
    '            txtKode.Clear()
    '        End If
    '    End If
    'End Sub
End Class
