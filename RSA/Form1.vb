Public Class Form1

    Private Function Phi(ByVal z As Long) 'euler phi function which finds the number of integers that are relatively prime to n
        Dim count, i As Long
        For i = 1 To z - 1
            If gcd(i, z) = 1 Then
                count += 1
            End If
        Next
        Return count
    End Function

    Private Function gcd(ByVal n As Long, ByVal m As Long) As Long 'returns the greatest common denominator
        Dim r, p, q As Integer
        p = Math.Max(n, m)
        q = Math.Min(n, m)
        r = p Mod q
        Do While r > 0
            p = q
            q = r
            r = p Mod q
        Loop
        Return q
    End Function

    Private Function mul_inv(a As Long, n As Long) As Long 'finds the inverse in z<n> - decoding
        If n < 0 Then n = -n
        If a < 0 Then a = n - ((-a) Mod n)
        Dim t As Long : t = 0
        Dim nt As Long : nt = 1
        Dim r As Long : r = n
        Dim nr As Long : nr = a
        Dim q As Long
        Dim tmp As Long
        Do While nr <> 0
            q = r \ nr
            tmp = t
            t = nt
            nt = tmp - q * nt
            tmp = r
            r = nr
            nr = tmp - q * nr
        Loop
        If t < 0 Then t = t + n
        mul_inv = t
    End Function

    Private Sub ComputePowers(ByVal n As Integer, ByVal maxp As Integer, modulus As Integer) ' a function that allows us to compute c= = M^e mod n -decoding
        Dim power, exponent As Integer
        Dim output As String
        output = ""
        power = 1
        For exponent = 1 To maxp
            power = power * n Mod modulus
            powers(exponent) = power
            output = power & vbCrLf
        Next exponent
        TextBox14.Text += output
        TextBox13.Text = ""
        TextBox12.Visible = True
    End Sub

    Private Sub ComputePoers(ByVal n As Integer, ByVal maxp As Integer, modulus As Integer) ' computes M = c^d mod N, where M is the decoded number - decoding
        Dim power, exponent As Integer
        Dim output As String
        output = ""
        power = 1
        For exponent = 1 To maxp
            power = power * n Mod modulus
            powers(exponent) = power
            output = power & vbCrLf
        Next exponent
        TextBox18.Text += output
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim n As Long
        Dim phin As Integer
        Dim prime() As Integer = {101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293, 307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499, 503, 509, 521, 523, 541, 547, 557, 563, 569, 571, 577, 587, 593, 599, 601, 607, 613, 617, 619, 631, 641, 643, 647, 653, 659, 661, 673, 677, 683, 691, 701, 709, 719, 727, 733, 739, 743, 751, 757, 761, 769, 773, 787, 797, 809, 811, 821, 823, 827, 829, 839, 853, 857, 859, 863, 877, 881, 883, 887, 907, 911, 919, 929, 937, 941, 947, 953, 967, 971, 977, 983, 991, 997}
        Dim prime2() As Integer = {101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293, 307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499, 503, 509, 521, 523, 541, 547, 557, 563, 569, 571, 577, 587, 593, 599, 601, 607, 613, 617, 619, 631, 641, 643, 647, 653, 659, 661, 673, 677, 683, 691, 701, 709, 719, 727, 733, 739, 743, 751, 757, 761, 769, 773, 787, 797, 809, 811, 821, 823, 827, 829, 839, 853, 857, 859, 863, 877, 881, 883, 887, 907, 911, 919, 929, 937, 941, 947, 953, 967, 971, 977, 983, 991, 997}
        Dim p As Integer = prime(New Random().Next(0, prime.Length - 1)) ' picks between the Primes listed above
        Dim q As Integer = p
        Do Until p <> q  'since we need two distinct primes, this loop was needed
            q = prime(New Random().Next(0, prime.Length - 1))
        Loop
        n = p * q
        TextBox1.Text = p
        TextBox2.Text = q
        TextBox3.Text = n
        phin = Phi(n)
        TextBox4.Text = phin
        Dim e1 As Integer
        If TextBox4.Text = phin Then ' allows to enter the e
            MessageBox.Show(" To construct your public and private keys, pick a number for e. " & vbCrLf & vbCrLf & " Such that e is relatively prime to φ (n) " & vbCrLf & vbCrLf & " Use Check button to see if e is valid ")
            System.Threading.Thread.Sleep(500)
            e1 = InputBox("Enter a number for e")
            TextBox5.Text = e1
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click ' finds d, where e*d congruent to 1 mod phi(n) - decoding
        Dim n1 As Integer
        Dim p As Integer
        Dim d As Integer
        Dim e1 As Integer
        Dim n0 As Integer
        p = TextBox4.Text
        n0 = TextBox3.Text
        n1 = TextBox3.Text
        e1 = TextBox5.Text
        TextBox7.Text = e1
        TextBox6.Text = n0
        TextBox9.Text = n1
        d = mul_inv(e1, p)
        TextBox8.Text = d
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click ' restates the public key, so that the inputted letter could be encoded - encoding public key
        Dim n2, e2 As Integer
        n2 = TextBox9.Text
        e2 = TextBox7.Text
        TextBox10.Text = n2
        TextBox11.Text = e2
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim arr() As Char = TextBox12.Text.ToCharArray 'translator for the encoder. If a letter its typed, it will translate it to a corresponding digit so that digit could be encoded. - encoding
        For Each cr As Char In arr
            If cr = "A" Or cr = "a" Then
                TextBox13.AppendText("001")
            ElseIf cr = "B" Or cr = "b" Then
                TextBox13.AppendText("002")
            ElseIf cr = "C" Or cr = "c" Then
                TextBox13.AppendText("003")
            ElseIf cr = "D" Or cr = "d" Then
                TextBox13.AppendText("004")
            ElseIf cr = "E" Or cr = "e" Then
                TextBox13.AppendText("005")
            ElseIf cr = "F" Or cr = "f" Then
                TextBox13.AppendText("006")
            ElseIf cr = "G" Or cr = "g" Then
                TextBox13.AppendText("007")
            ElseIf cr = "H" Or cr = "h" Then
                TextBox13.AppendText("008")
            ElseIf cr = "I" Or cr = "i" Then
                TextBox13.AppendText("009")
            ElseIf cr = "J" Or cr = "j" Then
                TextBox13.AppendText("010")
            ElseIf cr = "K" Or cr = "k" Then
                TextBox13.AppendText("011")
            ElseIf cr = "L" Or cr = "l" Then
                TextBox13.AppendText("012")
            ElseIf cr = "M" Or cr = "m" Then
                TextBox13.AppendText("013")
            ElseIf cr = "N" Or cr = "n" Then
                TextBox13.AppendText("014")
            ElseIf cr = "O" Or cr = "o" Then
                TextBox13.AppendText("015")
            ElseIf cr = "P" Or cr = "p" Then
                TextBox13.AppendText("016")
            ElseIf cr = "Q" Or cr = "q" Then
                TextBox13.AppendText("017")
            ElseIf cr = "R" Or cr = "r" Then
                TextBox13.AppendText("018")
            ElseIf cr = "S" Or cr = "s" Then
                TextBox13.AppendText("019")
            ElseIf cr = "T" Or cr = "t" Then
                TextBox13.AppendText("020")
            ElseIf cr = "U" Or cr = "u" Then
                TextBox13.AppendText("021")
            ElseIf cr = "V" Or cr = "v" Then
                TextBox13.AppendText("022")
            ElseIf cr = "W" Or cr = "w" Then
                TextBox13.AppendText("023")
            ElseIf cr = "X" Or cr = "x" Then
                TextBox13.AppendText("024")
            ElseIf cr = "Y" Or cr = "y" Then
                TextBox13.AppendText("025")
            ElseIf cr = "Z" Or cr = "z" Then
                TextBox13.AppendText("026")
            ElseIf cr = " " Then
                TextBox13.AppendText("027")
            End If
            TextBox12.Text = ""
            TextBox12.Visible = False
        Next
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click 'computes c = M^e mod n , where C is the encoded number. - encoding
        Dim c, ee, n As Integer
        c = TextBox13.Text
        ee = TextBox11.Text
        n = TextBox10.Text
        ComputePowers(c, ee, n)
    End Sub

    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click 'prompts to enter private key
        Dim n, d
        n = InputBox("Enter the n of your private key")
        TextBox16.Text = n
        d = InputBox("Enter the d of your private key")
        TextBox15.Text = d
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click 'decodes to get M, where M = c^d mod n
        Dim c, d, n As Integer
        c = TextBox17.Text
        d = TextBox15.Text
        n = TextBox16.Text
        ComputePoers(c, d, n)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        TextBox18.Text = ""
        TextBox17.Text = ""
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click 'checks to see if e and phi(n) are co prime
        Dim echeck As Boolean
        Dim check As Integer
        Dim e1 As Integer
        check = gcd(TextBox4.Text, TextBox5.Text)
        If check = 1 Then
            TextBox19.Text = check
            MessageBox.Show("e is relatively prime to φ (n), you can now generate your public and private keys")
            echeck = True
        Else
            MessageBox.Show("e is not relatively prime to φ (n) ")
            e1 = InputBox("Enter a number for e")
            TextBox5.Text = e1
        End If
        If echeck = True Then
            Button2.Visible = True
        End If
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        TextBox14.Text = ""
        TextBox13.Text = ""
        TextBox12.Text = ""
        TextBox12.Visible = True
    End Sub

    Dim powers(10000) As Integer

End Class
