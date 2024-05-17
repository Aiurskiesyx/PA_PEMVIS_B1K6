Imports MySql.Data.MySqlClient

Public Class JanjiTemuPasien
    Private Sub JanjiTemuPasien_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
    End Sub
    Sub Kosong()
        cbSpesialis.SelectedIndex = -1
        cbDokter.SelectedIndex = -1
        cbJam.SelectedIndex = -1
        cbTanggal.SelectedIndex = -1
    End Sub
    Private Sub LoadTanggal(namaDokter As String)
        Dim query As String = "SELECT DISTINCT tanggal FROM tbjadwaldokter WHERE namaDokter = @namaDokter"
        Dim cmd As New MySqlCommand(query, CONN)
        cmd.Parameters.AddWithValue("@namaDokter", namaDokter)
        Dim reader As MySqlDataReader = cmd.ExecuteReader()

        cbTanggal.Items.Clear()

        While reader.Read()
            cbTanggal.Items.Add(reader.GetDateTime("tanggal").ToString("yyyy-MM-dd"))
        End While
        reader.Close()
    End Sub
    Private Sub LoadJam(idDokter As String, tanggal As Date)
        Dim query As String = "SELECT jam FROM tbjadwaldokter WHERE idDokter = @idDokter AND tanggal = @tanggal"
        Dim cmd As New MySqlCommand(query, CONN)
        cmd.Parameters.AddWithValue("@idDokter", idDokter)
        cmd.Parameters.AddWithValue("@tanggal", tanggal)
        Dim reader As MySqlDataReader = cmd.ExecuteReader()

        cbJam.Items.Clear()

        While reader.Read()
            Dim jam As String = reader.GetString("jam")
            Dim jamArray() As String = jam.Split(",")

            For Each timeSlot As String In jamArray
                Dim trimmedTimeSlot As String = timeSlot.Trim()
                If Not cbJam.Items.Contains(trimmedTimeSlot) Then
                    cbJam.Items.Add(trimmedTimeSlot)
                End If
            Next
        End While
        reader.Close()
    End Sub
    Private Sub cbSpesialis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSpesialis.SelectedIndexChanged
        If cbSpesialis.SelectedItem IsNot Nothing Then
            Dim selectedSpesialis As String = cbSpesialis.SelectedItem.ToString()
            LoadDokterBySpesialis(selectedSpesialis)
        End If
    End Sub
    Private Sub LoadDokterBySpesialis(spesialis As String)
        Dim query As String = "SELECT DISTINCT namaDokter FROM tbjadwaldokter WHERE spesialis = @spesialis"
        Dim cmd As New MySqlCommand(query, CONN)
        cmd.Parameters.AddWithValue("@spesialis", spesialis)
        Dim reader As MySqlDataReader = cmd.ExecuteReader()
        cbDokter.Items.Clear()

        While reader.Read()
            cbDokter.Items.Add(reader.GetString("namaDokter"))
        End While

        reader.Close()
    End Sub
    Private Sub cbDokter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbDokter.SelectedIndexChanged
        If cbDokter.SelectedItem IsNot Nothing Then
            Dim idDokter As String = GetDokterID(cbDokter.SelectedItem.ToString())
            LoadTanggal(cbDokter.SelectedItem.ToString())
        End If
    End Sub
    Private Function GetDokterIDByDate(namaDokter As String, tanggal As Date) As String
        Dim query As String = "SELECT idDokter FROM tbjadwaldokter WHERE namaDokter = @namaDokter AND tanggal = @tanggal"
        Dim cmd As New MySqlCommand(query, CONN)
        cmd.Parameters.AddWithValue("@namaDokter", namaDokter)
        cmd.Parameters.AddWithValue("@tanggal", tanggal)
        Dim idDokter As String = Convert.ToString(cmd.ExecuteScalar())
        Return idDokter
    End Function
    Private Sub cbTanggal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbTanggal.SelectedIndexChanged
        If cbTanggal.SelectedItem IsNot Nothing AndAlso cbDokter.SelectedItem IsNot Nothing Then
            Try
                Dim selectedDate As Date = Date.Parse(cbTanggal.SelectedItem.ToString())
                Dim selectedDokter As String = cbDokter.SelectedItem.ToString()

                Dim idDokter As String = GetDokterIDByDate(selectedDokter, selectedDate)

                LoadJam(idDokter, selectedDate)
            Catch ex As Exception
                MessageBox.Show("Terjadi kesalahan saat memuat jam: " & ex.Message)
            End Try
        End If
    End Sub
    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Try
            If cbDokter.SelectedItem Is Nothing OrElse cbTanggal.SelectedItem Is Nothing OrElse cbJam.SelectedItem Is Nothing Then
                MessageBox.Show("Harap lengkapi semua pilihan.")
                Return
            End If

            Dim selectedDokter As String = cbDokter.SelectedItem.ToString()
            Dim selectedTanggal As String = cbTanggal.SelectedItem.ToString()
            Dim selectedJam As String = cbJam.SelectedItem.ToString()


            Dim idAkun As Integer = Convert.ToInt32(Login.globalId)
            Dim queryCheck As String = "SELECT COUNT(*) FROM tbjanjikonsul WHERE idAkun = @idAkun AND DATE(tanggal) = @tanggal"
            Dim cmdCheck As New MySqlCommand(queryCheck, CONN)
            cmdCheck.Parameters.AddWithValue("@idAkun", idAkun)
            cmdCheck.Parameters.AddWithValue("@tanggal", selectedTanggal)
            Dim count As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())
            If count > 0 Then
                MessageBox.Show("Anda sudah membuat janji temu hari ini.")
                Return
            End If

            If jamPenuh(idAkun, selectedDokter, selectedTanggal, selectedJam) Then
                MessageBox.Show("Jam yang dipilih sudah ada janji temu.")
                Return
            End If
            Dim query As String = "INSERT INTO tbjanjikonsul (idAkun, idDokter, tanggal, jam) VALUES (@idAkun, @idDokter, @tanggal, @jam)"
            Dim cmd As New MySqlCommand(query, CONN)

            Dim idDokter As String = GetDokterID(selectedDokter)

            cmd.Parameters.AddWithValue("@idAkun", idAkun)
            cmd.Parameters.AddWithValue("@idDokter", idDokter)
            cmd.Parameters.AddWithValue("@tanggal", selectedTanggal)
            cmd.Parameters.AddWithValue("@jam", selectedJam)

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If rowsAffected > 0 Then
                MessageBox.Show("Janji Konsultasi Berhasil Dibuat!!!.")
                Kosong()
            Else
                MessageBox.Show(" Janji Konsultasi Gagal Dibuat!!!")
            End If
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)
        End Try
    End Sub
    Private Function jamPenuh(idAkun As Integer, namaDokter As String, tanggal As Date, jam As String) As Boolean
        Dim idDokter As String = GetDokterID(namaDokter)
        Dim query As String = "SELECT COUNT(*) FROM tbjanjikonsul WHERE idDokter = @idDokter AND tanggal = @tanggal AND jam = @jam AND idAkun <> @idAkun"
        Dim cmd As New MySqlCommand(query, CONN)

        cmd.Parameters.AddWithValue("@idAkun", idAkun)
        cmd.Parameters.AddWithValue("@idDokter", idDokter)
        cmd.Parameters.AddWithValue("@tanggal", tanggal)
        cmd.Parameters.AddWithValue("@jam", jam)

        Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
        Return count > 0
    End Function
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Hide()
        MenuPasien.Show()
    End Sub
    Private Function GetDokterID(namaDokter As String) As String ' Ubah tipe data kembalian menjadi String
        Dim query As String = "SELECT idDokter FROM tbjadwaldokter WHERE namaDokter = @namaDokter"
        Dim cmd As New MySqlCommand(query, CONN)
        cmd.Parameters.AddWithValue("@namaDokter", namaDokter)
        Dim idDokter As String = Convert.ToString(cmd.ExecuteScalar()) ' Ubah tipe data variabel idDokter menjadi String
        Return idDokter
    End Function
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Kosong()
    End Sub
End Class