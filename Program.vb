
Module Program

    Sub Main()

        DatabaseHelper.InitializeDatabase()

        Dim choice As Integer

        Do
            Console.Clear()

            Console.WriteLine("========================================")
            Console.WriteLine("       STUDENT MANAGEMENT SYSTEM")
            Console.WriteLine("========================================")
            Console.WriteLine("1. Add Student")
            Console.WriteLine("2. View All Students")
            Console.WriteLine("3. Search Student")
            Console.WriteLine("4. Update Student")
            Console.WriteLine("5. Delete Student")
            Console.WriteLine("6. Exit")
            Console.WriteLine("========================================")
            Console.Write("Enter your choice: ")

            If Not Integer.TryParse(Console.ReadLine(), choice) Then
                Console.WriteLine("Invalid input! Enter a number.")
                Console.ReadKey()
                Continue Do
            End If

            Select Case choice

                Case 1
                    AddStudent()

                Case 2
                    ViewStudents()

                Case 3
                    SearchStudent()

                Case 4
                    UpdateStudent()

                Case 5
                    DeleteStudent()

                Case 6
                    Console.WriteLine("Thank you for using the system!")

                Case Else
                    Console.WriteLine("Invalid choice!")
                    Console.ReadKey()

            End Select

        Loop While choice <> 6

    End Sub

    ' ADD STUDENT
    Sub AddStudent()

        Console.Clear()
        Console.WriteLine("========== ADD STUDENT ==========")

        Dim s As New Student()

        s.StudentID = ReadStudentID()

        If DatabaseHelper.FindStudent(s.StudentID) IsNot Nothing Then
            Console.WriteLine("Student ID already exists!")
            Console.ReadKey()
            Return
        End If

        s.StudentName = ReadRequiredInput("Enter Student Name: ")
        s.Course = ReadRequiredInput("Enter Course: ")
        s.Email = ReadRequiredInput("Enter Email: ")

        DatabaseHelper.AddStudent(s)

        Console.WriteLine(vbCrLf & "Student added successfully!")
        Console.ReadKey()

    End Sub

    ' VIEW ALL STUDENTS
    Sub ViewStudents()

        Console.Clear()

        Console.WriteLine("========== ALL STUDENT RECORDS ==========")

        Dim students = DatabaseHelper.GetAllStudents()

        If students.Count = 0 Then

            Console.WriteLine("No student records found.")

        Else

            For Each s In students
                DisplayStudent(s)
            Next

            Console.WriteLine("Total Students: " & students.Count)

        End If

        Console.ReadKey()

    End Sub

    ' SEARCH STUDENT
    Sub SearchStudent()

        Console.Clear()

        Console.WriteLine("========== SEARCH STUDENT ==========")

        Dim id As Integer = ReadStudentID()

        Dim s = DatabaseHelper.FindStudent(id)

        If s IsNot Nothing Then

            Console.WriteLine(vbCrLf & "Student Found!")
            DisplayStudent(s)

        Else

            Console.WriteLine("Student not found!")

        End If

        Console.ReadKey()

    End Sub

    ' UPDATE STUDENT
    Sub UpdateStudent()

        Console.Clear()

        Console.WriteLine("========== UPDATE STUDENT ==========")

        Dim id As Integer = ReadStudentID()

        Dim s = DatabaseHelper.FindStudent(id)

        If s IsNot Nothing Then

            Console.WriteLine("Current Student Details:")
            DisplayStudent(s)

            Console.WriteLine(vbCrLf & "Enter Updated Details:")

            s.StudentName = ReadRequiredInput("Enter New Name: ")
            s.Course = ReadRequiredInput("Enter New Course: ")
            s.Email = ReadRequiredInput("Enter New Email: ")

            DatabaseHelper.UpdateStudent(s)

            Console.WriteLine(vbCrLf & "Student updated successfully!")

        Else

            Console.WriteLine("Student not found!")

        End If

        Console.ReadKey()

    End Sub

    ' DELETE STUDENT
    Sub DeleteStudent()

        Console.Clear()

        Console.WriteLine("========== DELETE STUDENT ==========")

        Dim id As Integer = ReadStudentID()

        Dim s = DatabaseHelper.FindStudent(id)

        If s IsNot Nothing Then

            Console.WriteLine(vbCrLf & "Student Details:")
            DisplayStudent(s)

            Console.Write("Are you sure you want to delete? (Y/N): ")

            Dim confirmation As String =
                If(Console.ReadLine(), "").Trim().ToUpperInvariant()

            If confirmation = "Y" Then

                DatabaseHelper.DeleteStudent(id)
                Console.WriteLine("Student deleted successfully!")

            Else

                Console.WriteLine("Deletion cancelled.")

            End If

        Else

            Console.WriteLine("Student not found!")

        End If

        Console.ReadKey()

    End Sub

    ' DISPLAY STUDENT DETAILS
    Sub DisplayStudent(s As Student)

        Console.WriteLine("----------------------------------------")
        Console.WriteLine("Student ID   : " & s.StudentID)
        Console.WriteLine("Student Name : " & s.StudentName)
        Console.WriteLine("Course       : " & s.Course)
        Console.WriteLine("Email        : " & s.Email)
        Console.WriteLine("----------------------------------------")

    End Sub

    ' VALIDATE REQUIRED INPUT
    Function ReadRequiredInput(prompt As String) As String

        Dim value As String = ""

        Do

            Console.Write(prompt)
            value = If(Console.ReadLine(), "").Trim()

            If String.IsNullOrWhiteSpace(value) Then
                Console.WriteLine("This field cannot be empty!")
            End If

        Loop While String.IsNullOrWhiteSpace(value)

        Return value

    End Function

    ' VALIDATE STUDENT ID
    Function ReadStudentID() As Integer

        Dim id As Integer

        Do

            Console.Write("Enter Student ID: ")

            If Integer.TryParse(Console.ReadLine(), id) AndAlso id > 0 Then
                Return id
            End If

            Console.WriteLine("Enter a valid positive Student ID!")

        Loop

    End Function

End Module
