Imports MySql.Data.MySqlClient

Public Class TambahCatatan
    Sub AturDataGridView()
        DataGridView1.Columns("idJanji").HeaderText = "Kode"
        DataGridView1.Columns("namaDokter").HeaderText = "Nama Dokter"
        DataGridView1.Columns("nama").HeaderText = "Nama Pasien"
        DataGridView1.Columns("tanggal").HeaderText = "Tanggal"
        DataGridView1.Columns("jam").HeaderText = "Jam"

        ' Menambahkan kolom "Catatan" dengan tombol view
        Dim buttonLihatCatatan As New DataGridViewButtonColumn()
        buttonLihatCatatan.HeaderText = "Catatan"
        buttonLihatCatatan.Text = "View"
        buttonLihatCatatan.UseColumnTextForButtonValue = True
        DataGridView1.Columns.Add(buttonLihatCatatan)
    End Sub
    Private Sub TampilCatatan()
        koneksi()
        Dim query As String = "SELECT tbjanjikonsul.idJanji, tbjadwaldokter.namaDokter, tbakun.nama, tbjanjikonsul.tanggal, tbjanjikonsul.jam " &
                              "FROM tbjanjikonsul " &
                              "INNER JOIN tbjadwaldokter ON tbjanjikonsul.idDokter = tbjadwaldokter.idDokter " &
                              "INNER JOIN tbakun ON tbjanjikonsul.idAkun = tbakun.id"
        Using cmd As New MySqlCommand(query, CONN)
            Dim da As New MySqlDataAdapter(cmd)
            Dim ds As New DataSet
            da.Fill(ds, "tbjanjikonsul")
            DataGridView1.DataSource = ds.Tables("tbjanjikonsul")
        End Using
    End Sub
    Private Sub TambahCatatan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TampilCatatan()
        AturDataGridView()
    End Sub
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 5 AndAlso e.RowIndex >= 0 Then
            Dim idjanji As Integer = DataGridView1.Rows(e.RowIndex).Cells("idjanji").Value
            Dim formInputCatatan As New InputCatatan()
            formInputCatatan.SelectedIdJanjiKonsultasi = idJanji
            formInputCatatan.Show()
        End If
    End Sub
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Hide()
        MenuAdmin.Show()
    End Sub
End Class
