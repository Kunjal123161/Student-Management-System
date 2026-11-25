
Imports Microsoft.Data.Sqlite
Imports System.IO

Public Module DatabaseHelper

    Private ReadOnly dbPath As String =
        Path.Combine(Directory.GetCurrentDirectory(), "students.db")

    Private Function GetConnection() As SqliteConnection
        Return New SqliteConnection("Data Source=" & dbPath)
    End Function

    Public Sub InitializeDatabase()

        Using con = GetConnection()

            con.Open()

            Dim query As String =
                "CREATE TABLE IF NOT EXISTS Students (" &
                "StudentID INTEGER PRIMARY KEY, " &
                "StudentName TEXT NOT NULL, " &
                "Course TEXT, " &
                "Email TEXT)"

            Using cmd As New SqliteCommand(query, con)
                cmd.ExecuteNonQuery()
            End Using

        End Using

    End Sub

    Public Sub AddStudent(s As Student)

        Using con = GetConnection()

            con.Open()

            Dim query As String =
                "INSERT INTO Students VALUES (@id, @name, @course, @email)"

            Using cmd As New SqliteCommand(query, con)

                cmd.Parameters.AddWithValue("@id", s.StudentID)
                cmd.Parameters.AddWithValue("@name", s.StudentName)
                cmd.Parameters.AddWithValue("@course", s.Course)
                cmd.Parameters.AddWithValue("@email", s.Email)

                cmd.ExecuteNonQuery()

            End Using

        End Using

    End Sub

    Public Function GetAllStudents() As List(Of Student)

        Dim result As New List(Of Student)

        Using con = GetConnection()

            con.Open()

            Dim query As String = "SELECT * FROM Students"

            Using cmd As New SqliteCommand(query, con)
                Using reader = cmd.ExecuteReader()

                    While reader.Read()

                        Dim s As New Student With {
                            .StudentID = reader.GetInt32(0),
                            .StudentName = reader.GetString(1),
                            .Course = reader.GetString(2),
                            .Email = reader.GetString(3)
                        }

                        result.Add(s)

                    End While

                End Using
            End Using

        End Using

        Return result

    End Function

    Public Function FindStudent(id As Integer) As Student

        Using con = GetConnection()

            con.Open()

            Dim query As String =
                "SELECT * FROM Students WHERE StudentID = @id"

            Using cmd As New SqliteCommand(query, con)

                cmd.Parameters.AddWithValue("@id", id)

                Using reader = cmd.ExecuteReader()

                    If reader.Read() Then

                        Return New Student With {
                            .StudentID = reader.GetInt32(0),
                            .StudentName = reader.GetString(1),
                            .Course = reader.GetString(2),
                            .Email = reader.GetString(3)
                        }

                    End If

                End Using

            End Using

        End Using

        Return Nothing

    End Function

    Public Sub UpdateStudent(s As Student)

        Using con = GetConnection()

            con.Open()

            Dim query As String =
                "UPDATE Students SET StudentName=@name, " &
                "Course=@course, Email=@email WHERE StudentID=@id"

            Using cmd As New SqliteCommand(query, con)

                cmd.Parameters.AddWithValue("@id", s.StudentID)
                cmd.Parameters.AddWithValue("@name", s.StudentName)
                cmd.Parameters.AddWithValue("@course", s.Course)
                cmd.Parameters.AddWithValue("@email", s.Email)

                cmd.ExecuteNonQuery()

            End Using

        End Using

    End Sub

    Public Sub DeleteStudent(id As Integer)

        Using con = GetConnection()

            con.Open()

            Dim query As String =
                "DELETE FROM Students WHERE StudentID=@id"

            Using cmd As New SqliteCommand(query, con)

                cmd.Parameters.AddWithValue("@id", id)
                cmd.ExecuteNonQuery()

            End Using

        End Using

    End Sub

End Module
