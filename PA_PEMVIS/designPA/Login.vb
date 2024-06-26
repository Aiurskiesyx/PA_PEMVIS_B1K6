﻿Imports MySql.Data.MySqlClient

Public Class Login
    Public Shared globalId As Integer

    Sub Kosong()
        txtPw.Clear()
        txtUsn.Clear()
        txtUsn.Focus()
        txtPw.Focus()
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim username As String = txtUsn.Text.Trim()
        Dim password As String = txtPw.Text.Trim()

        Try
            If username.ToLower() = "admin" AndAlso password = "123" Then
                loginSuccess("admin")
            Else
                Dim id As Integer = authenticateUser(username, password)
                If id <> 0 Then
                    globalId = id
                End If
            End If
        Catch ex As Exception
            MsgBox("Terjadi kesalahan: " & ex.Message)
        End Try
    End Sub
    Private Function authenticateUser(username As String, password As String) As Integer
        Dim id As Integer = 0
        koneksi()
        Dim query As String = "SELECT id FROM tbakun WHERE username = @username AND password = @password" ' Query untuk autentikasi
        Dim CMD As New MySqlCommand(query, CONN)
        CMD.Parameters.AddWithValue("@username", username)
        CMD.Parameters.AddWithValue("@password", password)

        Dim RD As MySqlDataReader = Nothing
        Try
            RD = CMD.ExecuteReader()
            If RD.Read() Then
                id = RD.GetInt32(0)
                loginSuccess("pasien")
            Else
                MsgBox("Username atau password salah.")
            End If
        Catch ex As Exception
            MsgBox("Terjadi kesalahan: " & ex.Message)
        Finally
            If RD IsNot Nothing Then
                RD.Close()
            End If
            CMD.Dispose()
        End Try
        Return id
    End Function


    Private Sub loginSuccess(role As String)
        If role = "admin" Then
            MenuAdmin.Show()
        ElseIf role = "pasien" Then
            MenuPasien.username.Text = txtUsn.Text()
            JanjiTemuPasien.username.Text = txtUsn.Text()
            MenuPasien.Show()
        Else
            MsgBox("Tidak ada peran yang cocok untuk pengguna ini.")
        End If
        Me.Hide()
    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Kosong()
    End Sub

    Private Sub btnRegis_Click(sender As Object, e As EventArgs) Handles btnRegis.Click
        Me.Hide()
        Registrasi.Show()
        Kosong()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub cbShowPass_CheckedChanged(sender As Object, e As EventArgs) Handles cbShowPass.CheckedChanged
        If cbShowPass.Checked Then
            txtPw.PasswordChar = ControlChars.NullChar
        Else
            txtPw.PasswordChar = "*"c
        End If
    End Sub
End Class
