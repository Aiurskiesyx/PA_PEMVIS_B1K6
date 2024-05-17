Imports MySql.Data.MySqlClient

Public Class Registrasi

    Private registeredUserID As Integer
    Sub Kosong()
        txtNama.Clear()
        txtPassword.Clear()
        txtUsername.Clear()
        rbLaki.Checked = False
        rbPerempuan.Checked = False
        cbTerm.Checked = False
        txtNama.Focus()
    End Sub

    Private Sub Registrasi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
        Kosong()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Me.Hide()
        Login.Show()
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If Not cbTerm.Checked Then
            MsgBox("Anda harus menyetujui Terms & Conditions sebelum melanjutkan.")
            Return
        End If

        Dim isTextBoxFilled As Boolean = Not String.IsNullOrWhiteSpace(txtNama.Text) AndAlso
                                         Not String.IsNullOrWhiteSpace(txtUsername.Text) AndAlso
                                         Not String.IsNullOrWhiteSpace(txtPassword.Text) AndAlso
                                         (rbLaki.Checked OrElse rbPerempuan.Checked)

        If Not isTextBoxFilled Then
            MsgBox("Data belum lengkap. Pastikan semua kolom telah diisi.")
            Return
        End If

        Try
            CMD = New MySqlCommand("SELECT COUNT(*) FROM tbakun WHERE username = @username", CONN)
            CMD.Parameters.AddWithValue("@username", txtUsername.Text)
            Dim count As Integer = Convert.ToInt32(CMD.ExecuteScalar())

            If count > 0 Then
                MsgBox("Username sudah digunakan. Silakan gunakan username lain.")
            Else
                Dim gender As String = If(rbLaki.Checked, "Laki-Laki", "Perempuan")
                Dim query As String = "INSERT INTO tbakun (nama, tglLahir, jenisKelamin, username, password) VALUES (@nama, @tglLahir, @jenisKelamin, @username, @password)"
                CMD = New MySqlCommand(query, CONN)
                CMD.Parameters.AddWithValue("@nama", txtNama.Text)
                CMD.Parameters.AddWithValue("@tglLahir", DateTimePicker1.Value.ToString("yyyy-MM-dd"))
                CMD.Parameters.AddWithValue("@jenisKelamin", gender)
                CMD.Parameters.AddWithValue("@username", txtUsername.Text)
                CMD.Parameters.AddWithValue("@password", txtPassword.Text)
                CMD.ExecuteNonQuery()

                registeredUserID = CInt(CMD.LastInsertedId)

                MsgBox("Registrasi berhasil. Akun telah dibuat.")
                Kosong()
            End If
        Catch ex As Exception
            MsgBox("Terjadi kesalahan: " & ex.Message)
        End Try
    End Sub
    Public Function GetRegisteredUserID() As Integer
        Return registeredUserID
    End Function
    Public Sub HanyaHuruf(e As KeyPressEventArgs)
        Dim tombol As Integer = Asc(e.KeyChar)
        If Not (((tombol >= 65) And (tombol <= 90)) Or ((tombol >= 97) And
       (tombol <= 122)) Or (tombol = 8) Or (tombol = 32) Or (tombol = 46)) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtNama_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNama.KeyPress
        HanyaHuruf(e)
    End Sub
End Class
