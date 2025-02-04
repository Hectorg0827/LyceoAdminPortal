Imports System.Windows.Forms

Public Class DashboardForm
    Inherits Form

    Private WithEvents btnAddStudent As New Button()
    Private dgvStudents As New DataGridView()
    Private txtStudentName As New TextBox()
    Private txtGrade As New TextBox()
    Private txtAttendance As New TextBox()
    Private lblStudentName As New Label()
    Private lblGrade As New Label()
    Private lblAttendance As New Label()

    Private Students As New List(Of Student)

    Public Sub New()
        Me.Text = "Dashboard"
        Me.Size = New Drawing.Size(600, 400)

        ' Set up the DataGridView
        dgvStudents.Location = New Drawing.Point(20, 20)
        dgvStudents.Size = New Drawing.Size(540, 200)
        Me.Controls.Add(dgvStudents)

        ' Labels and TextBoxes for adding a new student
        lblStudentName.Text = "Name:"
        lblStudentName.Location = New Drawing.Point(20, 240)
        Me.Controls.Add(lblStudentName)

        txtStudentName.Location = New Drawing.Point(80, 240)
        Me.Controls.Add(txtStudentName)

        lblGrade.Text = "Grade:"
        lblGrade.Location = New Drawing.Point(20, 270)
        Me.Controls.Add(lblGrade)

        txtGrade.Location = New Drawing.Point(80, 270)
        Me.Controls.Add(txtGrade)

        lblAttendance.Text = "Attendance:"
        lblAttendance.Location = New Drawing.Point(20, 300)
        Me.Controls.Add(lblAttendance)

        txtAttendance.Location = New Drawing.Point(80, 300)
        Me.Controls.Add(txtAttendance)

        btnAddStudent.Text = "Add Student"
        btnAddStudent.Location = New Drawing.Point(80, 330)
        Me.Controls.Add(btnAddStudent)
    End Sub

    Private Sub DashboardForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Initialize with some demo data
        Students.Add(New Student With {.ID = 1, .Name = "Alice Johnson", .Grade = "A", .Attendance = 95})
        Students.Add(New Student With {.ID = 2, .Name = "Bob Smith", .Grade = "B", .Attendance = 87})
        Students.Add(New Student With {.ID = 3, .Name = "Charlie Lee", .Grade = "C", .Attendance = 92})
        BindStudentData()
    End Sub

    Private Sub BindStudentData()
        dgvStudents.DataSource = Nothing
        dgvStudents.DataSource = Students.Select(Function(s) New With {
                                                  Key .ID = s.ID,
                                                  Key .Name = s.Name,
                                                  Key .Grade = s.Grade,
                                                  Key .Attendance = s.Attendance
                                              }).ToList()
    End Sub

    Private Sub btnAddStudent_Click(sender As Object, e As EventArgs) Handles btnAddStudent.Click
        Dim studentName As String = txtStudentName.Text.Trim()
        Dim grade As String = txtGrade.Text.Trim()
        Dim attendance As Integer

        If studentName = "" OrElse grade = "" OrElse Not Integer.TryParse(txtAttendance.Text.Trim(), attendance) Then
            MessageBox.Show("Please enter valid student details.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim newID As Integer = If(Students.Count > 0, Students.Max(Function(s) s.ID) + 1, 1)
        Students.Add(New Student With {.ID = newID, .Name = studentName, .Grade = grade, .Attendance = attendance})
        BindStudentData()

        txtStudentName.Clear()
        txtGrade.Clear()
        txtAttendance.Clear()
    End Sub

    Private Sub DashboardForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Application.Exit()
    End Sub
End Class
