Imports MySql.Data.MySqlClient

Public Class InputCatatan
    Public Property SelectedIdJanjiKonsultasi As Integer

    Private Sub InputCatatan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadExistingData()
    End Sub

    Private Sub LoadExistingData()
        koneksi()
        Dim query As String = "SELECT catatan FROM tbjanjikonsul WHERE idJanji = @idJanji"
        Using CMD As New MySqlCommand(query, CONN)
            CMD.Parameters.AddWithValue("@idJanji", SelectedIdJanjiKonsultasi)

            Using RD As MySqlDataReader = CMD.ExecuteReader()
                If RD.Read() Then
                    TextBox1.Text = RD("catatan").ToString()
                End If
            End Using
        End Using
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim catatan As String = TextBox1.Text.Trim()
        If String.IsNullOrEmpty(catatan) Then
            MessageBox.Show("Catatan tidak boleh kosong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        koneksi()
        Dim query As String = "UPDATE tbjanjikonsul SET catatan = @catatan WHERE idJanji = @idJanji"
        Using CMD As New MySqlCommand(query, CONN)
            CMD.Parameters.AddWithValue("@catatan", catatan)
            CMD.Parameters.AddWithValue("@idJanji", SelectedIdJanjiKonsultasi)

            Dim rowsAffected As Integer = CMD.ExecuteNonQuery()
            If rowsAffected > 0 Then
                MessageBox.Show("Catatan berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            Else
                MessageBox.Show("Gagal menyimpan catatan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Using
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Hide()
        TambahCatatan.Show()
    End Sub
End Class
