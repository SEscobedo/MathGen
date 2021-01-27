Module GramaticaEspa�ola

    'Esta funci�n determina si se trata de una vocal o una consonante o ninguna de las dos
    Function Letra(ByVal letr As String) As String

        Dim s As String = "xx"
        If letr Like "[aeiou������]" Then
            s = "vo" 'vocal
        ElseIf letr Like "[bcdfghjklmn�pqrstvwzyz]" Then
            s = "co" 'consonante
        End If

        Return s
    End Function

    Function SepararS�labas(ByVal palabra As String) As String()
        Dim i As Long
        Dim res As String, silaba As String
        res = palabra

        For i = 1 To Len(palabra)
            silaba = Mid$(palabra, i, 4)
            If silaba Like "[aeiou������][!aeiou������][aeiou������]*" Then
                res = Mid$(res, i, 1) & "/" & Mid$(res, i + 1)
            ElseIf silaba Like "[aeiou������][!aeiou������][!aeiou������][aeiou������]" Then
                res = Mid$(res, i, 2) & "/" & Mid$(res, i + 2)
            End If
        Next
        SepararS�labas = Split(res, "/")
    End Function

    'ART�CULOS **********************************************************************************
    'Esta funci�n da el art�culo correspondiente
    'NOTA 1: el art�culo se debuelve en min�sculas.
    Function Art�culo(ByVal forma As String) As String
        Dim res As String
        'determinados
        If forma = "dsm" Then
            res = "el"
        ElseIf forma = "dsf" Then
            res = "la"
        ElseIf forma = "dsn" Then
            res = "lo"
        ElseIf forma = "dpm" Then
            res = "los"
        ElseIf forma = "dpf" Then
            res = "las"
            'indeterminados
        ElseIf forma = "ism" Then
            res = "un"
        ElseIf forma = "isf" Then
            res = "una"
        ElseIf forma = "ipm" Then
            res = "unos"
        ElseIf forma = "ipf" Then
            res = "unas"
        Else
            res = ""
        End If
        Art�culo = res

        'SEP. 2010, OK.
    End Function

    'Esta funci�n da el art�culo correspondiente
    'NOTA 1: art debe estar en min�sculas.
    'NOTA 2: si art no es un art�culo la funci�n responde "Error".
    Function RECArt�culo(ByVal art As String) As String
        Dim res As String
        'determinados
        If art = "el" Then
            res = "dsm"
        ElseIf art = "la" Then
            res = "dsf"
        ElseIf art = "lo" Then
            res = "dsn"
        ElseIf art = "los" Then
            res = "dpm"
        ElseIf art = "las" Then
            res = "dpf"
            'indeterminados
        ElseIf art = "un" Then
            res = "ism"
        ElseIf art = "una" Then
            res = "isf"
        ElseIf art = "unos" Then
            res = "ipm"
        ElseIf art = "unas" Then
            res = "ipf"
        Else
            res = "Error"
        End If

        RECArt�culo = res

        'SEP. 2010, OK. Modif. DIC. 2010
    End Function

    '**************************************************************************
    'NOMBRES SUSTANTIVOS **********************************************************************************

    'Esta funci�n da el plural de un nombre
    'todav�a no funciona bien: cami�n --> cami�nes

    Function Plural(ByVal nombre As String) As String
        'excepciones
        If nombre = "pap�" Or nombre = "mam�" Then
            nombre = nombre & "s"
        ElseIf nombre Like "*s" Then
            'si no son agudos quedan igual <-- falta agregar esa regla
        ElseIf nombre Like "*[aeiou��]" Then
            nombre = nombre & "s"
        ElseIf nombre Like "*[!aeiou�z]" Then
            nombre = nombre & "es"
        ElseIf nombre Like "*z" Then
            nombre = Mid$(nombre, 1, Len(nombre) - 1) & "ces"
        End If

        Plural = nombre
    End Function
    'Esta funci�n da el plural de una palabra o serie de palabras


    Function Singular(ByVal s As String) As String
        If s Like "* *" Then
            Dim A = Split(s, " ")
            Dim i As Long, res = ""

            For i = 0 To UBound(A)
                res = res & " " & Singular(A(i))
            Next

            s = res

        Else 's es una sola palabra
            If s Like "*ces" Then
                s = Left(s, Len(s) - 3) & "z"
            ElseIf s Like "*[ns]es" And Not s Like "*[�����]*" Then 'aguda terminada en n o s.
                Dim i As Long
                s = Left(s, Len(s) - 2)


                For i = 1 To Len(s)
                    Select Case Mid$(s, Len(s) - i, 1)
                        Case "a"
                            Mid$(s, Len(s) - i, 1) = "�"
                            Exit For
                        Case "e"
                            Mid$(s, Len(s) - i, 1) = "�"
                            Exit For
                        Case "i"
                            Mid$(s, Len(s) - i, 1) = "�"
                            Exit For
                        Case "o"
                            Mid$(s, Len(s) - i, 1) = "�"
                            Exit For
                        Case "u"
                            Mid$(s, Len(s) - i, 1) = "�"
                            Exit For
                    End Select

                Next
            ElseIf s Like "*ores" Or s Like "*ales" Then
                s = Left(s, Len(s) - 2)
            ElseIf s Like "*[aeiou��]s" Then
                s = Left(s, Len(s) - 1)
            ElseIf s Like "*[!aeiou�z]es" Then
                s = Left(s, Len(s) - 2)
            ElseIf s Like "*sis" Then
                'queda igual
            ElseIf s Like "*s" Then
                s = Left(s, Len(s) - 1)
            End If
        End If
        Singular = Trim(s)
    End Function

    'NOMBRES ADJETIVOS **********************************************************************************
    Function Demostrat(ByVal forma As String) As String
        Dim res As String

        If forma = "sm1" Then
            res = "este"
        ElseIf forma = "sm2" Then
            res = "ese"
        ElseIf forma = "sm3" Then
            res = "aquel"
        ElseIf forma = "sf1" Then
            res = "esta"
        ElseIf forma = "sf2" Then
            res = "esa"
        ElseIf forma = "sf3" Then
            res = "aquella"
            'plural
        ElseIf forma = "pm1" Then
            res = "estos"
        ElseIf forma = "pm2" Then
            res = "esos"
        ElseIf forma = "pm3" Then
            res = "aquellos"
        ElseIf forma = "pf1" Then
            res = "estas"
        ElseIf forma = "pf2" Then
            res = "esas"
        ElseIf forma = "pf3" Then
            res = "aquellas"
        Else
            res = ""
        End If

        Demostrat = res
    End Function

    Function RECDemostrat(ByVal forma As String) As String
        Dim res As String

        If forma = "este" Then
            res = "sm1"
        ElseIf forma = "ese" Then
            res = "sm2"
        ElseIf forma = "aquel" Then
            res = "sm3"
        ElseIf forma = "esta" Then
            res = "sf1"
        ElseIf forma = "esa" Then
            res = "sf2"
        ElseIf forma = "aquella" Then
            res = "sf3"
            'plural
        ElseIf forma = "estos" Then
            res = "pm1"
        ElseIf forma = "esos" Then
            res = "pm2"
        ElseIf forma = "aquellos" Then
            res = "pm3"
        ElseIf forma = "estas" Then
            res = "pf1"
        ElseIf forma = "esas" Then
            res = "pf2"
        ElseIf forma = "aquellas" Then
            res = "pf3"
        Else
            res = "Error"
        End If

        RECDemostrat = res
    End Function

    'PRONOMBRES **********************************************************************************

    Function Pronombre(ByVal forma As String) As String
        Dim res As String

        'personales
        'nominativo
        'Select Case forma
        '   Case "Psn1n"
        'res = "yo"
        ' Case "Psn2n"
        'res = "t�")
        ' End Select

        If forma = "Psn1n" Then
            res = "yo"
        ElseIf forma = "Psn2n" Then
            res = "t�"
        ElseIf forma = "Psm3n" Then
            res = "�l"
        ElseIf forma = "Psf3n" Then
            res = "ella"
        ElseIf forma = "Ppm1n" Then
            res = "nosotros"
        ElseIf forma = "Ppf1n" Then
            res = "nosotras"
        ElseIf forma = "Ppn2n" Then
            res = "ustedes"
        ElseIf forma = "Ppm3n" Then
            res = "ellos"
        ElseIf forma = "Ppf3n" Then
            res = "ellas"

            'acusativo
        ElseIf forma = "Ps1a" Then
            res = "a m�"
        ElseIf forma = "Ps2a" Then
            res = "a t�"
        ElseIf forma = "Psm3a" Then
            res = "a �l"
        ElseIf forma = "Psf3a" Then
            res = "a ella"
        ElseIf forma = "Ppm1a" Then
            res = "a nosotros"
        ElseIf forma = "Ppf1a" Then
            res = "a nosotras"
        ElseIf forma = "Ppn2a" Then
            res = "a ustedes"
        ElseIf forma = "Ppm3a" Then
            res = "a ellos"
        ElseIf forma = "Ppf3a" Then
            res = "a ellas"

            'relativos = R
        ElseIf forma = "R1" Then
            res = "que"
        ElseIf forma = "R2" Then
            res = "cual"
        ElseIf forma = "R3" Then
            res = "quien"
        ElseIf forma = "R4" Then
            res = "cuyo"
        ElseIf forma = "R5" Then
            res = "cuanto"

            'indefinidos = I (faltan)
            'referentes a personas = Ip
        ElseIf forma = "Ic1" Then
            res = "alguien"
        ElseIf forma = "Ic2" Then
            res = "nadie"
        ElseIf forma = "Ic3" Then
            res = "cualquiera"
        ElseIf forma = "Ic4" Then
            res = "quienquiera"
            'referentes a cosas = Ic
        ElseIf forma = "Ic1" Then
            res = "algo"
        ElseIf forma = "Ic2" Then
            res = "nada"

        Else
            res = ""
        End If

        Pronombre = res
    End Function

    Function RECPronombre(ByVal forma As String) As String
        Dim res As String

        Select Case forma
            'pron. relativo
            Case "que"
                res = "R1"
            Case "cual"
                res = "R2"
            Case "quien"
                res = "R3"
            Case "cuyo"
                res = "R4"
            Case "cuanto"
                res = "R5"
                '--
            Case "donde"
                res = "R6"
            Case "cuando"
                res = "R7"

                'pron. personal

            Case "yo"
                res = "=Y"
            Case "t�"
                res = "=U"
            Case "�l"
                res = "=E"
            Case "�lla"
                res = "=A"
            Case "nosotros"
                res = "=Yp"
            Case "ustedes"
                res = "=Up"
            Case "ellos"
                res = "=Ep"
            Case "ellas"
                res = "=Ap"

                'pron. posesivo
            Case "mi"
                res = "PP1"
            Case "tu"
                res = "PP2"
            Case "su"
                res = "PP3"
            Case "nuestro"
                res = "PP1m"
            Case "nuestra"
                res = "PP1f"
            Case "nuestros"
                res = "PP1mp"
            Case "nuestras"
                res = "PP1fp"

            Case Else
                res = "Error"
        End Select


        RECPronombre = res
    End Function

    Function RECPrep(ByVal prep As String) As String
        Dim res As String
        Select Case prep

            Case "a"            'h
                res = "Ph"
            Case "en"           's
                res = "Ps"
            Case "de"           't
                res = "Pt"

            Case "por"          'principio
                res = "Pp"
            Case "para"         'fin
                res = "Pf"

            Case "sin"          'no-medio
                res = "Pn"
            Case "con"          'medio
                res = "Pm"

            Case "contra"       'anti-orientaci�n
                res = "Pc"
            Case "hacia"        'orientaci�n
                res = "Pi"

                'PARCIALES y otras
            Case "ante", "bajo", "cabe", "sobre", "tras", "entre" 'partes de "en"
                res = "Ps0"

            Case "desde", "hasta"
                res = "P1"

            Case "excepto", "salvo"
                res = "P2"

            Case "seg�n"
                res = "P3"

            Case Else
                res = "Error"

        End Select

        RECPrep = res
    End Function

    Function RECCuan(ByVal prep As String) As String
        Dim res As String
        Select Case prep

            Case "todo", "alg�n", "ning�n", "toda", "alguna", "ninguna", "cada"
                res = "C"
            Case "todos", "algunos", "todas", "algunas"
                res = "PCr"
            Case "algunos", "algunas"
                res = "PCp"
            Case "un", "una", "el", "la", "lo", "los", "las", "unos", "unas", "varias", "varios"
                res = "PC"
            Case "cierto", "cierta"
                res = "PC/"
            Case Else
                res = "Error"
        End Select

        RECCuan = res

    End Function

    'reconoce part�cula (conectvas o conexiones de per�odo)
    Function RECPart(ByVal prep As String) As String
        Dim res As String
        Select Case prep

            Case "pero", "embargo", "obstante"
                res = "Adr"
            Case "porque"
                res = "QM"
            Case "y", "e"
                res = "Y"
            Case "o", "u"
                res = "O"
            Case "si"
                res = "CND"
            Case "entonces"
                res = "CNS"
            Case Else
                res = "Error"
        End Select

        RECPart = res

    End Function

    Function RECExpr(ByVal prep As String) As String
        Dim res As String
        Dim cad = "igualmente"
        If prep = cad Then
            res = "EX"
        Else
            res = "Error"
        End If

        RECExpr = res

    End Function

    Function RECinsult(ByVal palabra As String) As String
        Dim res As String

        Dim d = ",est�pido,est�pida,tonto,tonta,tarado,tarada,menso,mensa,idiota,tarugo,taruga,mentecato,mentecata,inepto,inepta,imbecil,baboso,babosa"

        If InStr(d, "," & palabra & ",", CompareMethod.Text) Then
            res = "SLT"
        Else
            res = "Error"
        End If
        RECinsult = res
    End Function

    Function RECVco(ByVal verbo As String) As String
        Dim res As String

        Select Case verbo
            Case "es"
                res = "3s"
            Case "soy"
                res = "1s"
            Case "somos"
                res = "2p"
            Case "son"
                res = "3p"
            Case "eres"
                res = "2s"
            Case "era"
                res = "3sp"
            Case "�ramos"
                res = "1pp"
            Case "eran"
                res = "3pp"
            Case "eras"
                res = "2sp"
            Case "ser�"
                res = "3sf"
            Case "ser�"
                res = "1sf"
            Case "ser�n"
                res = "3pf"
            Case "seremos"
                res = "1pf"
            Case "ser�s"
                res = "2sf"
            Case "fui"
                res = "1SP"
            Case "fuiste"
                res = "2SP"
            Case "fue"
                res = "3SP"
            Case "fuimos"
                res = "1PP"
            Case "fueron"
                res = "4PP" '4 = 2 � 3.
            Case "estoy"
                res = "e1"
            Case "est�s"
                res = "e2"
            Case "est�"
                res = "e3"
            Case "estamos"
                res = "e1p"
            Case "est�n"
                res = "e4p"
            Case Else
                res = "Error"
        End Select

        RECVco = res
    End Function

    'reconoce un verbo en infinitivo
    Function RECVerbo(ByVal verbo As String) As String
        Dim res As String
        Dim d = ",nadir,primer,muladar,lugar,ayer,anteayer,"

        If verbo Like "*er" Or verbo Like "*ir" Or verbo Like "*ar" Then

            res = "VI"
            If InStr(d, "," & verbo & ",", CompareMethod.Text) Then res = "Error"
        Else
            res = ("Error")
        End If

        RECVerbo = res
    End Function

    Function RECVconj(ByVal verbo As String) As String
        Dim res As String
        Dim FormasVerbales As String
        FormasVerbales = ",naci�,nace,ha nacido,nacer�,corre,corri�,corr�a,va,vien,fue,ha sido,vi�,oy�,escribe,escribi�,ha escrito,escribir�,"
        If InStr(FormasVerbales, "," & verbo & ",", CompareMethod.Text) Then
            res = "VC"
        Else
            res = "Error"
        End If
        RECVconj = res
    End Function

    Function RECFecha(ByVal expr As String) As String
        Dim res As String
        If expr = "setiembre" Then expr = "septiembre"
        Dim FormasVerbales(4) As String
        FormasVerbales(0) = ",enero,febrero,marzo,abril,mayo,junio,julio,agosto,septiembre,octubre,noviembre,diciembre,"
        FormasVerbales(1) = ",lunes,martes,mi�rcoles,jueves,viernes,s�bado,domingo,ma�ana,ayer,anteayer"
        FormasVerbales(2) = ",minuto,hora,d�a,semana,mes,a�o,lustro,sexenio,d�cada,siglo,centuria,milenio,�poca,tiempo,"
        FormasVerbales(4) = ",minutos,horas,d�as,semanas,meses,a�os,lustros,sexenios,d�cadas,siglos,centurias,milenios,�pocas,"
        FormasVerbales(3) = ",primavera,verano,est�o,oto�o,invierno,"
        If InStr(FormasVerbales(0), "," & expr & ",", CompareMethod.Text) Then
            Dim A = Split(FormasVerbales(0), ",")
            Dim i As Long
            For i = 0 To UBound(A)
                If A(i) = expr Then
                    res = "FM" & Str(i) : Exit For
                End If
            Next


        ElseIf InStr(FormasVerbales(1), "," & expr & ",", CompareMethod.Text) Then
            Dim A = Split(FormasVerbales(1), ",")
            Dim i As Long
            For i = 0 To UBound(A)
                If A(i) = expr Then
                    res = "FS" & Str(i) : Exit For
                End If
            Next

        ElseIf InStr(FormasVerbales(2), "," & expr & ",", CompareMethod.Text) Then
            res = "FTs"
        ElseIf InStr(FormasVerbales(4), "," & expr & ",", CompareMethod.Text) Then
            res = "FTp"
        ElseIf InStr(FormasVerbales(3), "," & expr & ",", CompareMethod.Text) Then
            res = "FE"
        Else
            res = "Error"
        End If

        RECFecha = res
    End Function

    Function RECN�mero(ByVal expr As String) As String
        Dim res As String
        Dim flag As Boolean
        Dim i As Long

        For i = 1 To Len(expr)
            If Mid(expr, i, 1) Like "[0-9]" Then
                flag = True
            Else
                flag = False
                Exit For
            End If
        Next
        If flag Then RECN�mero = expr : Exit Function

        Select Case expr
            Case "uno"
                res = "1"
            Case "dos"
                res = "2"
            Case "tres"
                res = "3"
            Case "cuatro"
                res = "4"
            Case "cinco"
                res = "5"
            Case "seis"
                res = "6"
            Case "siete"
                res = "7"
            Case "ocho"
                res = "8"
            Case "nueve"
                res = "9"
            Case "diez"
                res = "10"
            Case "once"
                res = "11"
            Case "doce"
                res = "12"
            Case "trece"
                res = "13"
            Case "catorce"
                res = "14"
            Case "quince"
                res = "15"
            Case "diecis�is"
                res = "16"
            Case "diecisiete"
                res = "17"
            Case "cien"
                res = "100"
            Case "doscentos"
                res = "200"
            Case "mil"
                res = "1000"

                'ordinales

            Case "primero", "primer"
                res = "1�"
            Case "segundo"
                res = "2�"
            Case "tercero", "tercer"
                res = "3�"
            Case "cuarto"
                res = "4�"
            Case "quinto"
                res = "5�"
            Case "sexto"
                res = "6�"
            Case "s�ptimo"
                res = "7�"
            Case "octavo"
                res = "8�"
            Case "noveno"
                res = "9�"
            Case "d�cimo"
                res = "10�"
            Case "und�cimo"
                res = "11�"
            Case "duod�cimo"
                res = "12�"

            Case "cent�simo"
                res = "100�"

            Case "mil�simo"
                res = "1000"

            Case Else
                res = "Error"
        End Select


        RECN�mero = res
    End Function

    'VERBOS **********************************************************************************
    'esta funci�n recibe un verbo en forma infinitiva y lo debuevle con la conjugaci�n especificada
    'versi�n 1.2
    Function Verbo(ByVal v As String, ByVal forma As String) As String
        Dim f As String, r As String, v2 As String
        f = ""
        v2 = v 'en caso de error 

#Region "formas num�ricas"
        'para formas num�ricas
        Select Case forma
            Case "1"
                forma = "p1s"
            Case "2"
                forma = "p1p"
            Case "3"
                forma = "p2s"
            Case "4"
                forma = "p2p"
            Case "5"
                forma = "p3s"
            Case "6"
                forma = "p3p"
            Case "7"
                forma = "r1s"
            Case "8"
                forma = "r1p"
            Case "9"
                forma = "r2s"
            Case "10"
                forma = "r2p"
            Case "11"
                forma = "r3s"
            Case "12"
                forma = "r3p"
            Case "13"
                forma = "f1s"
            Case "14"
                forma = "f1p"
            Case "15"
                forma = "f2s"
            Case "16"
                forma = "f2p"
            Case "17"
                forma = "f3s"
            Case "18"
                forma = "f3p"
            Case "19"
                forma = "c1s"
            Case "20"
                forma = "c1p"
            Case "21"
                forma = "c2s"
            Case "22"
                forma = "c2p"
            Case "23"
                forma = "c3s"
            Case "24"
                forma = "c3p"
            Case "25"
                forma = "a1s"
            Case "26"
                forma = "a1p"
            Case "27"
                forma = "a2s"
            Case "28"
                forma = "a2p"
            Case "29"
                forma = "a3s"
            Case "30"
                forma = "a3p"
            Case "31"
                forma = "b1s"
            Case "32"
                forma = "b1p"
            Case "33"
                forma = "b2s"
            Case "34"
                forma = "b2p"
            Case "35"
                forma = "b3s"
            Case "36"
                forma = "b3p"
            Case "37"
                forma = "u1s"
            Case "38"
                forma = "u1p"
            Case "39"
                forma = "u2s"
            Case "40"
                forma = "u2p"
            Case "41"
                forma = "u3s"
            Case "42"
                forma = "u3p"

        End Select
#End Region

        'formas irregulares: verbo ser

#Region "Verbos terminados en -er"
        '-------------------------------------------------------------
        If v Like "*?er" Then
            'prever para correcci�n de irregularidades
            If v Like "*cer" Then f = "cer"
            If v Like "*eer" Then f = "eer"
            If v Like "*ger" Then f = "ger"

            'obtener ra�z del verbo
            v = Mid$(v, 1, Len(v) - 2)
            r = v

            'conjugar
            'MODO INDICATIVO
            'TIEMPOS SIMPLES
            'Presente
            If forma = "p1s" Then
                v = v & "o" 'presente
            ElseIf forma = "p1p" Then
                v = v & "emos" 'plural
            ElseIf forma = "p2s" Then
                v = v & "es" 'presente
            ElseIf forma = "p2p" Then
                v = v & "en" 'presente
            ElseIf forma = "p3s" Then
                v = v & "e" 'presente
            ElseIf forma = "p3p" Then
                v = v & "en" 'presente

                'pasado
            ElseIf forma = "r1s" Then
                v = v & "�" 'pasdo
            ElseIf forma = "r1p" Then
                v = v & "imos" 'pasdo
            ElseIf forma = "r2s" Then
                v = v & "iste" 'pasdo
            ElseIf forma = "r2p" Then
                v = v & "ieron" 'pasdo
            ElseIf forma = "r3s" Then
                v = v & "i�" 'pasdo
            ElseIf forma = "r3p" Then
                v = v & "ieron" 'pasado

                'futuro
            ElseIf forma = "f1s" Then
                v = v & "er�" 'futuro
            ElseIf forma = "f1p" Then
                v = v & "er�mos" 'futuro
            ElseIf forma = "f2s" Then
                v = v & "er�s" 'futuro
            ElseIf forma = "f2p" Then
                v = v & "er�n" 'futuro
            ElseIf forma = "f3s" Then
                v = v & "er�" 'futuro
            ElseIf forma = "f3p" Then
                v = v & "er�n" 'futuro

                'copret�rito
            ElseIf forma = "c1s" Then
                v = v & "�a"
            ElseIf forma = "c1p" Then
                v = v & "�amos"
            ElseIf forma = "c2s" Then
                v = v & "�as"
            ElseIf forma = "c2p" Then
                v = v & "�an"
            ElseIf forma = "c3s" Then
                v = v & "�a"
            ElseIf forma = "c3p" Then
                v = v & "�an"
                'TIEMPOS COMPUESTOS
                'Antepresente
            ElseIf forma = "a1s" Then
                v = "he " & v & "ido"
            ElseIf forma = "a1p" Then
                v = "hemos " & v & "ido"
            ElseIf forma = "a2s" Then
                v = "has " & v & "ido"
            ElseIf forma = "a2p" Then
                v = "han " & v & "ido"
            ElseIf forma = "a3s" Then
                v = "ha " & v & "ido"
            ElseIf forma = "a3p" Then
                v = "han " & v & "ido"
                'antepret�rito
            ElseIf forma = "b1s" Then
                v = "hube " & v & "ido"
            ElseIf forma = "b1p" Then
                v = "hubimos " & v & "ido"
            ElseIf forma = "b2s" Then
                v = "hubiste " & v & "ido"
            ElseIf forma = "b2p" Then
                v = "hubieron " & v & "ido"
            ElseIf forma = "b3s" Then
                v = "hubo " & v & "ido"
            ElseIf forma = "b3p" Then
                v = "hubieron " & v & "ido"
                'antefuturo
            ElseIf forma = "u1s" Then
                v = "habr� " & v & "ido"
            ElseIf forma = "u1p" Then
                v = "habremos " & v & "ido"
            ElseIf forma = "u2s" Then
                v = "habr�s " & v & "ido"
            ElseIf forma = "u2p" Then
                v = "habr�n " & v & "ido"
            ElseIf forma = "u3s" Then
                v = "habr� " & v & "ido"
            ElseIf forma = "u3p" Then
                v = "habr�n " & v & "ido"
                'antecopret�rito
            ElseIf forma = "v1s" Then
                v = "hab�a " & v & "ido"
            ElseIf forma = "v1p" Then
                v = "ha�amos " & v & "ido"
            ElseIf forma = "v2s" Then
                v = "hab�as " & v & "ido"
            ElseIf forma = "v2p" Then
                v = "hab�an " & v & "ido"
            ElseIf forma = "v3s" Then
                v = "hab�a " & v & "ido"
            ElseIf forma = "v3p" Then
                v = "hab�an " & v & "ido"

                'MODO POTENCIAL
                'simple
            ElseIf forma = "Ps1s" Then
                v = v & "er�a"
            ElseIf forma = "Ps1p" Then
                v = v & "er�amos"
            ElseIf forma = "Ps2s" Then
                v = v & "er�as"
            ElseIf forma = "Ps2p" Then
                v = v & "er�an"
            ElseIf forma = "Ps3s" Then
                v = v & "er�a"
            ElseIf forma = "Ps1p" Then
                v = v & "er�an"
                'compuesto
            ElseIf forma = "Pc1s" Then
                v = "habr�a " & v & "ido"
            ElseIf forma = "Pc1p" Then
                v = "habr�amos " & v & "ido"
            ElseIf forma = "Pc2s" Then
                v = "habr�as " & v & "ido"
            ElseIf forma = "Pc2p" Then
                v = "habr�an " & v & "ido"
            ElseIf forma = "Pc3s" Then
                v = "habr�a " & v & "ido"
            ElseIf forma = "Pc3p" Then
                v = "habr�an " & v & "ido"
                'MODO SUBJUNTIVO
                'TIEMPOS SIMPLES
                'presente
            ElseIf forma = "Sp1s" Then
                v = v & "a"
            ElseIf forma = "Sp1p" Then
                v = v & "amos"
            ElseIf forma = "Sp2s" Then
                v = v & "as"
            ElseIf forma = "Sp2p" Then
                v = v & "an"
            ElseIf forma = "Sp3s" Then
                v = v & "a"
            ElseIf forma = "Sp3p" Then
                v = v & "an"
                'pret�rito
                '1era forma
            ElseIf forma = "Sr1s" Then
                v = v & "iera"
            ElseIf forma = "Sr1p" Then
                v = v & "i�ramos"
            ElseIf forma = "Sr2s" Then
                v = v & "ieras"
            ElseIf forma = "Sr2p" Then
                v = v & "ieran"
            ElseIf forma = "Sr3s" Then
                v = v & "iera"
            ElseIf forma = "Sr3p" Then
                v = v & "ieran"
                '2da forma
            ElseIf forma = "Sr1s2" Then
                v = v & "iese"
            ElseIf forma = "Sr1p2" Then
                v = v & "i�semos"
            ElseIf forma = "Sr2s2" Then
                v = v & "ieses"
            ElseIf forma = "Sr2p2" Then
                v = v & "iesen"
            ElseIf forma = "Sr3s2" Then
                v = v & "iese"
            ElseIf forma = "Sr3p2" Then
                v = v & "iesen"
                'futuro
            ElseIf forma = "Sf1s" Then
                v = v & "iere"
            ElseIf forma = "Sf1p" Then
                v = v & "i�remos"
            ElseIf forma = "Sf2s" Then
                v = v & "ieres"
            ElseIf forma = "Sf2p" Then
                v = v & "ieren"
            ElseIf forma = "Sf3s" Then
                v = v & "iere"
            ElseIf forma = "Sf3p" Then
                v = v & "ieren"
                'compuestos

                'Antepresente
            ElseIf forma = "Sa1s" Then
                v = "haya " & v & "ido"
            ElseIf forma = "Sa1p" Then
                v = "hayamos " & v & "ido"
            ElseIf forma = "Sa2s" Then
                v = "hayas " & v & "ido"
            ElseIf forma = "Sa2p" Then
                v = "hayan " & v & "ido"
            ElseIf forma = "Sa3s" Then
                v = "haya " & v & "ido"
            ElseIf forma = "Sa3p" Then
                v = "hayan " & v & "ido"
                'antepret�rito
                '1era forma
            ElseIf forma = "Sb1s" Then
                v = "hubiera " & v & "ido"
            ElseIf forma = "Sb1p" Then
                v = "hubi�ramos " & v & "ido"
            ElseIf forma = "Sb2s" Then
                v = "hubieras " & v & "ido"
            ElseIf forma = "Sb2p" Then
                v = "hubieran " & v & "ido"
            ElseIf forma = "Sb3s" Then
                v = "hubiera " & v & "ido"
            ElseIf forma = "Sb3p" Then
                v = "hubieran " & v & "ido"
                '2da forma
            ElseIf forma = "Sb1s2" Then
                v = "hubiese " & v & "ido"
            ElseIf forma = "Sb1p2" Then
                v = "hubi�semos " & v & "ido"
            ElseIf forma = "Sb2s2" Then
                v = "hubieses " & v & "ido"
            ElseIf forma = "Sb2p2" Then
                v = "hubiesen " & v & "ido"
            ElseIf forma = "Sb3s2" Then
                v = "hubiese " & v & "ido"
            ElseIf forma = "Sb3p2" Then
                v = "hubiesen " & v & "ido"

                'antefuturo
            ElseIf forma = "Su1s" Then
                v = "hubiere " & v & "ido"
            ElseIf forma = "Su1p" Then
                v = "hubi�remos " & v & "ido"
            ElseIf forma = "Su2s" Then
                v = "hubieres " & v & "ido"
            ElseIf forma = "Su2p" Then
                v = "hubieren " & v & "ido"
            ElseIf forma = "Su3s" Then
                v = "hubiere " & v & "ido"
            ElseIf forma = "Su3p" Then
                v = "hubieren " & v & "ido"


                'MODO IMPERATIVO
                'presente
            ElseIf forma = "Mp#s" Then
                v = v & "e"
            ElseIf forma = "Mp#p" Then
                v = v & "an"
                'MODOS IMPERSONALES
                'GERUNDIO
            ElseIf forma = "Gs" Then
                v = v & "iendo"
            ElseIf forma = "Gc" Then
                v = "habiendo " & v & "ido"
                'PARTICIPIO
            ElseIf forma = "Ta" Then
                v = v & "iente"
            ElseIf forma = "Tp" Then
                v = v & "ido"


            End If

            'correccion de irregularidades
            If f = "cer" And (Mid$(v, Len(r) + 1, 1) = "a" Or Mid$(v, Len(r) + 1, 1) = "o") Then Mid$(v, Len(r), 1) = "z"
            If f = "ger" And (Mid$(v, Len(r) + 1, 1) = "a" Or Mid$(v, Len(r) + 1, 1) = "o") Then Mid$(v, Len(r), 1) = "j"
            If f = "eer" And v Like "*[aeiou������]i[aeiou������]*" Then v = Replace(v, "i", "y") '<-- falta afinar


#End Region

#Region "Verbos terminados en -ar"
            '----------------------------------------------------------------
        ElseIf v Like "*?ar" Then

            'prever para correcci�n de irregularidades
            If v Like "*car" Then f = "car"
            If v Like "*gar" Then f = "gar"
            If v Like "*zar" Then f = "zar"

            'obtener ra�z
            v = Mid$(v, 1, Len(v) - 2)
            r = v

            'conjugar
            'MODO INDICATIVO
            'TIEMPOS SIMPLES
            'Presente
            If forma = "p1s" Then 'yo
                v = v & "o" 'presente
            ElseIf forma = "p1p" Then 'nosotros
                v = v & "amos" 'plural
            ElseIf forma = "p2s" Then 't�
                v = v & "as" 'presente
            ElseIf forma = "p2p" Then 'ustedes
                v = v & "an" 'presente
            ElseIf forma = "p3s" Then '�l/ella
                v = v & "a" 'presente
            ElseIf forma = "p3p" Then 'ellos/ellas
                v = v & "an" 'presente

                'pasado
            ElseIf forma = "r1s" Then
                v = v & "�" 'pasdo
            ElseIf forma = "r1p" Then
                v = v & "imos" 'pasdo
            ElseIf forma = "r2s" Then
                v = v & "iste" 'pasdo
            ElseIf forma = "r2p" Then
                v = v & "ieron" 'pasdo
            ElseIf forma = "r3s" Then
                v = v & "i�" 'pasdo
            ElseIf forma = "r3p" Then
                v = v & "ieron" 'pasado

                'futuro
            ElseIf forma = "f1s" Then
                v = v & "er�" 'futuro
            ElseIf forma = "f1p" Then
                v = v & "er�mos" 'futuro
            ElseIf forma = "f2s" Then
                v = v & "er�s" 'futuro
            ElseIf forma = "f2p" Then
                v = v & "er�n" 'futuro
            ElseIf forma = "f3s" Then
                v = v & "er�" 'futuro
            ElseIf forma = "f3p" Then
                v = v & "er�n" 'futuro

                'copret�rito
            ElseIf forma = "c1s" Then
                v = v & "�a"
            ElseIf forma = "c1p" Then
                v = v & "�amos"
            ElseIf forma = "c2s" Then
                v = v & "�as"
            ElseIf forma = "c2p" Then
                v = v & "�an"
            ElseIf forma = "c3s" Then
                v = v & "�a"
            ElseIf forma = "c3p" Then
                v = v & "�an"
                'TIEMPOS COMPUESTOS
                'Antepresente
            ElseIf forma = "a1s" Then
                v = "he " & v & "ido"
            ElseIf forma = "a1p" Then
                v = "hemos " & v & "ido"
            ElseIf forma = "a2s" Then
                v = "has " & v & "ido"
            ElseIf forma = "a2p" Then
                v = "han " & v & "ido"
            ElseIf forma = "a3s" Then
                v = "ha " & v & "ido"
            ElseIf forma = "a3p" Then
                v = "han " & v & "ido"
                'antepret�rito
            ElseIf forma = "b1s" Then
                v = "hube " & v & "ido"
            ElseIf forma = "b1p" Then
                v = "hubimos " & v & "ido"
            ElseIf forma = "b2s" Then
                v = "hubiste " & v & "ido"
            ElseIf forma = "b2p" Then
                v = "hubieron " & v & "ido"
            ElseIf forma = "b3s" Then
                v = "hubo " & v & "ido"
            ElseIf forma = "b3p" Then
                v = "hubieron " & v & "ido"
                'antefuturo
            ElseIf forma = "u1s" Then
                v = "habr� " & v & "ido"
            ElseIf forma = "u1p" Then
                v = "habremos " & v & "ido"
            ElseIf forma = "u2s" Then
                v = "habr�s " & v & "ido"
            ElseIf forma = "u2p" Then
                v = "habr�n " & v & "ido"
            ElseIf forma = "u3s" Then
                v = "habr� " & v & "ido"
            ElseIf forma = "u3p" Then
                v = "habr�n " & v & "ido"
                'antecopret�rito
            ElseIf forma = "v1s" Then
                v = "hab�a " & v & "ido"
            ElseIf forma = "v1p" Then
                v = "ha�amos " & v & "ido"
            ElseIf forma = "v2s" Then
                v = "hab�as " & v & "ido"
            ElseIf forma = "v2p" Then
                v = "hab�an " & v & "ido"
            ElseIf forma = "v3s" Then
                v = "hab�a " & v & "ido"
            ElseIf forma = "v3p" Then
                v = "hab�an " & v & "ido"

                'MODO POTENCIAL
                'simple
            ElseIf forma = "Ps1s" Then
                v = v & "er�a"
            ElseIf forma = "Ps1p" Then
                v = v & "er�amos"
            ElseIf forma = "Ps2s" Then
                v = v & "er�as"
            ElseIf forma = "Ps2p" Then
                v = v & "er�an"
            ElseIf forma = "Ps3s" Then
                v = v & "er�a"
            ElseIf forma = "Ps1p" Then
                v = v & "er�an"
                'compuesto
            ElseIf forma = "Pc1s" Then
                v = "habr�a " & v & "ido"
            ElseIf forma = "Pc1p" Then
                v = "habr�amos " & v & "ido"
            ElseIf forma = "Pc2s" Then
                v = "habr�as " & v & "ido"
            ElseIf forma = "Pc2p" Then
                v = "habr�an " & v & "ido"
            ElseIf forma = "Pc3s" Then
                v = "habr�a " & v & "ido"
            ElseIf forma = "Pc3p" Then
                v = "habr�an " & v & "ido"
                'MODO SUBJUNTIVO
                'TIEMPOS SIMPLES
                'presente
            ElseIf forma = "Sp1s" Then
                v = v & "a"
            ElseIf forma = "Sp1p" Then
                v = v & "amos"
            ElseIf forma = "Sp2s" Then
                v = v & "as"
            ElseIf forma = "Sp2p" Then
                v = v & "an"
            ElseIf forma = "Sp3s" Then
                v = v & "a"
            ElseIf forma = "Sp3p" Then
                v = v & "an"
                'pret�rito
                '1era forma
            ElseIf forma = "Sr1s" Then
                v = v & "iera"
            ElseIf forma = "Sr1p" Then
                v = v & "i�ramos"
            ElseIf forma = "Sr2s" Then
                v = v & "ieras"
            ElseIf forma = "Sr2p" Then
                v = v & "ieran"
            ElseIf forma = "Sr3s" Then
                v = v & "iera"
            ElseIf forma = "Sr3p" Then
                v = v & "ieran"
                '2da forma
            ElseIf forma = "Sr1s2" Then
                v = v & "iese"
            ElseIf forma = "Sr1p2" Then
                v = v & "i�semos"
            ElseIf forma = "Sr2s2" Then
                v = v & "ieses"
            ElseIf forma = "Sr2p2" Then
                v = v & "iesen"
            ElseIf forma = "Sr3s2" Then
                v = v & "iese"
            ElseIf forma = "Sr3p2" Then
                v = v & "iesen"
                'futuro
            ElseIf forma = "Sf1s" Then
                v = v & "iere"
            ElseIf forma = "Sf1p" Then
                v = v & "i�remos"
            ElseIf forma = "Sf2s" Then
                v = v & "ieres"
            ElseIf forma = "Sf2p" Then
                v = v & "ieren"
            ElseIf forma = "Sf3s" Then
                v = v & "iere"
            ElseIf forma = "Sf3p" Then
                v = v & "ieren"
                'compuestos

                'Antepresente
            ElseIf forma = "Sa1s" Then
                v = "haya " & v & "ido"
            ElseIf forma = "Sa1p" Then
                v = "hayamos " & v & "ido"
            ElseIf forma = "Sa2s" Then
                v = "hayas " & v & "ido"
            ElseIf forma = "Sa2p" Then
                v = "hayan " & v & "ido"
            ElseIf forma = "Sa3s" Then
                v = "haya " & v & "ido"
            ElseIf forma = "Sa3p" Then
                v = "hayan " & v & "ido"
                'antepret�rito
                '1era forma
            ElseIf forma = "Sb1s" Then
                v = "hubiera " & v & "ido"
            ElseIf forma = "Sb1p" Then
                v = "hubi�ramos " & v & "ido"
            ElseIf forma = "Sb2s" Then
                v = "hubieras " & v & "ido"
            ElseIf forma = "Sb2p" Then
                v = "hubieran " & v & "ido"
            ElseIf forma = "Sb3s" Then
                v = "hubiera " & v & "ido"
            ElseIf forma = "Sb3p" Then
                v = "hubieran " & v & "ido"
                '2da forma
            ElseIf forma = "Sb1s2" Then
                v = "hubiese " & v & "ido"
            ElseIf forma = "Sb1p2" Then
                v = "hubi�semos " & v & "ido"
            ElseIf forma = "Sb2s2" Then
                v = "hubieses " & v & "ido"
            ElseIf forma = "Sb2p2" Then
                v = "hubiesen " & v & "ido"
            ElseIf forma = "Sb3s2" Then
                v = "hubiese " & v & "ido"
            ElseIf forma = "Sb3p2" Then
                v = "hubiesen " & v & "ido"

                'antefuturo
            ElseIf forma = "Su1s" Then
                v = "hubiere " & v & "ido"
            ElseIf forma = "Su1p" Then
                v = "hubi�remos " & v & "ido"
            ElseIf forma = "Su2s" Then
                v = "hubieres " & v & "ido"
            ElseIf forma = "Su2p" Then
                v = "hubieren " & v & "ido"
            ElseIf forma = "Su3s" Then
                v = "hubiere " & v & "ido"
            ElseIf forma = "Su3p" Then
                v = "hubieren " & v & "ido"


                'MODO IMPERATIVO
                'presente
            ElseIf forma = "Mp#s" Then
                v = v & "e"
            ElseIf forma = "Mp#p" Then
                v = v & "an"
                'MODOS IMPERSONALES
                'GERUNDIO
            ElseIf forma = "Gs" Then
                v = v & "iendo"
            ElseIf forma = "Gc" Then
                v = "habiendo " & v & "ido"
                'PARTICIPIO
            ElseIf forma = "Ta" Then
                v = v & "iente"
            ElseIf forma = "Tp" Then
                v = v & "ido"


            End If

            'correccion de irregularidades
            If f = "car" And (Mid$(v, Len(r) + 1, 1) = "e") Then Mid$(v, Len(r), 1) = "qu"
            If f = "gar" And (Mid$(v, Len(r) + 1, 1) = "e") Then Mid$(v, Len(r), 1) = "gu"
            If f = "zar" And (Mid$(v, Len(r) + 1, 1) = "e") Then Mid$(v, Len(r), 1) = "c"

            '-----------------------------------------------------
#End Region

#Region "Verbos terminados en -ir"
        ElseIf v Like "*?ir" Then
            'prever para correcci�n de irregularidades
            If v Like "*cir" Then f = "cir"
            If v Like "*gir" Then f = "gir"
            If v Like "*guir" Then f = "guir"
            If v Like "*quir" Then f = "quir"

            'obtener ra�z
            v = Mid$(v, 1, Len(v) - 2)
            r = v
            'conjugar
            'MODO INDICATIVO
            'TIEMPOS SIMPLES
            'Presente
            If forma = "p1s" Then
                v = v & "o" 'presente
            ElseIf forma = "p1p" Then
                v = v & "imos" 'plural
            ElseIf forma = "p2s" Then
                v = v & "es" 'presente
            ElseIf forma = "p2p" Then
                v = v & "en" 'presente
            ElseIf forma = "p3s" Then
                v = v & "e" 'presente
            ElseIf forma = "p3p" Then
                v = v & "en" 'presente

                'pasado
            ElseIf forma = "r1s" Then
                v = v & "�" 'pasdo
            ElseIf forma = "r1p" Then
                v = v & "imos" 'pasdo
            ElseIf forma = "r2s" Then
                v = v & "iste" 'pasdo
            ElseIf forma = "r2p" Then
                v = v & "ieron" 'pasdo
            ElseIf forma = "r3s" Then
                v = v & "i�" 'pasdo
            ElseIf forma = "r3p" Then
                v = v & "ieron" 'pasado

                'futuro
            ElseIf forma = "f1s" Then
                v = v & "ir�" 'futuro
            ElseIf forma = "f1p" Then
                v = v & "ir�mos" 'futuro
            ElseIf forma = "f2s" Then
                v = v & "ir�s" 'futuro
            ElseIf forma = "f2p" Then
                v = v & "ir�n" 'futuro
            ElseIf forma = "f3s" Then
                v = v & "ir�" 'futuro
            ElseIf forma = "f3p" Then
                v = v & "ir�n" 'futuro

                'copret�rito
            ElseIf forma = "c1s" Then
                v = v & "�a"
            ElseIf forma = "c1p" Then
                v = v & "�amos"
            ElseIf forma = "c2s" Then
                v = v & "�as"
            ElseIf forma = "c2p" Then
                v = v & "�an"
            ElseIf forma = "c3s" Then
                v = v & "�a"
            ElseIf forma = "c3p" Then
                v = v & "�an"
                'TIEMPOS COMPUESTOS
                'Antepresente
            ElseIf forma = "a1s" Then
                v = "he " & v & "ido"
            ElseIf forma = "a1p" Then
                v = "hemos " & v & "ido"
            ElseIf forma = "a2s" Then
                v = "has " & v & "ido"
            ElseIf forma = "a2p" Then
                v = "han " & v & "ido"
            ElseIf forma = "a3s" Then
                v = "ha " & v & "ido"
            ElseIf forma = "a3p" Then
                v = "han " & v & "ido"
                'antepret�rito
            ElseIf forma = "b1s" Then
                v = "hube " & v & "ido"
            ElseIf forma = "b1p" Then
                v = "hubimos " & v & "ido"
            ElseIf forma = "b2s" Then
                v = "hubiste " & v & "ido"
            ElseIf forma = "b2p" Then
                v = "hubieron " & v & "ido"
            ElseIf forma = "b3s" Then
                v = "hubo " & v & "ido"
            ElseIf forma = "b3p" Then
                v = "hubieron " & v & "ido"
                'antefuturo
            ElseIf forma = "u1s" Then
                v = "habr� " & v & "ido"
            ElseIf forma = "u1p" Then
                v = "habremos " & v & "ido"
            ElseIf forma = "u2s" Then
                v = "habr�s " & v & "ido"
            ElseIf forma = "u2p" Then
                v = "habr�n " & v & "ido"
            ElseIf forma = "u3s" Then
                v = "habr� " & v & "ido"
            ElseIf forma = "u3p" Then
                v = "habr�n " & v & "ido"
                'antecopret�rito
            ElseIf forma = "v1s" Then
                v = "hab�a " & v & "ido"
            ElseIf forma = "v1p" Then
                v = "ha�amos " & v & "ido"
            ElseIf forma = "v2s" Then
                v = "hab�as " & v & "ido"
            ElseIf forma = "v2p" Then
                v = "hab�an " & v & "ido"
            ElseIf forma = "v3s" Then
                v = "hab�a " & v & "ido"
            ElseIf forma = "v3p" Then
                v = "hab�an " & v & "ido"

                'MODO POTENCIAL
                'simple
            ElseIf forma = "Ps1s" Then
                v = v & "ir�a"
            ElseIf forma = "Ps1p" Then
                v = v & "ir�amos"
            ElseIf forma = "Ps2s" Then
                v = v & "ir�as"
            ElseIf forma = "Ps2p" Then
                v = v & "ir�an"
            ElseIf forma = "Ps3s" Then
                v = v & "ir�a"
            ElseIf forma = "Ps1p" Then
                v = v & "ir�an"
                'compuesto
            ElseIf forma = "Pc1s" Then
                v = "habr�a " & v & "ido"
            ElseIf forma = "Pc1p" Then
                v = "habr�amos " & v & "ido"
            ElseIf forma = "Pc2s" Then
                v = "habr�as " & v & "ido"
            ElseIf forma = "Pc2p" Then
                v = "habr�an " & v & "ido"
            ElseIf forma = "Pc3s" Then
                v = "habr�a " & v & "ido"
            ElseIf forma = "Pc3p" Then
                v = "habr�an " & v & "ido"
                'MODO SUBJUNTIVO
                'TIEMPOS SIMPLES
                'presente
            ElseIf forma = "Sp1s" Then
                v = v & "a"
            ElseIf forma = "Sp1p" Then
                v = v & "amos"
            ElseIf forma = "Sp2s" Then
                v = v & "as"
            ElseIf forma = "Sp2p" Then
                v = v & "an"
            ElseIf forma = "Sp3s" Then
                v = v & "a"
            ElseIf forma = "Sp3p" Then
                v = v & "an"
                'pret�rito
                '1era forma
            ElseIf forma = "Sr1s" Then
                v = v & "iera"
            ElseIf forma = "Sr1p" Then
                v = v & "i�ramos"
            ElseIf forma = "Sr2s" Then
                v = v & "ieras"
            ElseIf forma = "Sr2p" Then
                v = v & "ieran"
            ElseIf forma = "Sr3s" Then
                v = v & "iera"
            ElseIf forma = "Sr3p" Then
                v = v & "ieran"
                '2da forma
            ElseIf forma = "Sr1s2" Then
                v = v & "iese"
            ElseIf forma = "Sr1p2" Then
                v = v & "i�semos"
            ElseIf forma = "Sr2s2" Then
                v = v & "ieses"
            ElseIf forma = "Sr2p2" Then
                v = v & "iesen"
            ElseIf forma = "Sr3s2" Then
                v = v & "iese"
            ElseIf forma = "Sr3p2" Then
                v = v & "iesen"
                'futuro
            ElseIf forma = "Sf1s" Then
                v = v & "iere"
            ElseIf forma = "Sf1p" Then
                v = v & "i�remos"
            ElseIf forma = "Sf2s" Then
                v = v & "ieres"
            ElseIf forma = "Sf2p" Then
                v = v & "ieren"
            ElseIf forma = "Sf3s" Then
                v = v & "iere"
            ElseIf forma = "Sf3p" Then
                v = v & "ieren"
                'compuestos

                'Antepresente
            ElseIf forma = "Sa1s" Then
                v = "haya " & v & "ido"
            ElseIf forma = "Sa1p" Then
                v = "hayamos " & v & "ido"
            ElseIf forma = "Sa2s" Then
                v = "hayas " & v & "ido"
            ElseIf forma = "Sa2p" Then
                v = "hayan " & v & "ido"
            ElseIf forma = "Sa3s" Then
                v = "haya " & v & "ido"
            ElseIf forma = "Sa3p" Then
                v = "hayan " & v & "ido"
                'antepret�rito
                '1era forma
            ElseIf forma = "Sb1s" Then
                v = "hubiera " & v & "ido"
            ElseIf forma = "Sb1p" Then
                v = "hubi�ramos " & v & "ido"
            ElseIf forma = "Sb2s" Then
                v = "hubieras " & v & "ido"
            ElseIf forma = "Sb2p" Then
                v = "hubieran " & v & "ido"
            ElseIf forma = "Sb3s" Then
                v = "hubiera " & v & "ido"
            ElseIf forma = "Sb3p" Then
                v = "hubieran " & v & "ido"
                '2da forma
            ElseIf forma = "Sb1s2" Then
                v = "hubiese " & v & "ido"
            ElseIf forma = "Sb1p2" Then
                v = "hubi�semos " & v & "ido"
            ElseIf forma = "Sb2s2" Then
                v = "hubieses " & v & "ido"
            ElseIf forma = "Sb2p2" Then
                v = "hubiesen " & v & "ido"
            ElseIf forma = "Sb3s2" Then
                v = "hubiese " & v & "ido"
            ElseIf forma = "Sb3p2" Then
                v = "hubiesen " & v & "ido"

                'antefuturo
            ElseIf forma = "Su1s" Then
                v = "hubiere " & v & "ido"
            ElseIf forma = "Su1p" Then
                v = "hubi�remos " & v & "ido"
            ElseIf forma = "Su2s" Then
                v = "hubieres " & v & "ido"
            ElseIf forma = "Su2p" Then
                v = "hubieren " & v & "ido"
            ElseIf forma = "Su3s" Then
                v = "hubiere " & v & "ido"
            ElseIf forma = "Su3p" Then
                v = "hubieren " & v & "ido"


                'MODO IMPERATIVO
                'presente
            ElseIf forma = "Mp#s" Then
                v = v & "e"
            ElseIf forma = "Mp#p" Then
                v = v & "an"
                'MODOS IMPERSONALES
                'INFINITIVO
            ElseIf forma = "Ns" Then
                v = v & "ir"
            ElseIf forma = "Nc" Then
                v = "haber " & v & "ido"
                'GERUNDIO
            ElseIf forma = "Gs" Then
                v = v & "iendo"
            ElseIf forma = "Gc" Then
                v = "habiendo " & v & "ido"
                'PARTICIPIO
            ElseIf forma = "Ta" Then
                v = v & "iente"
            ElseIf forma = "Tp" Then
                v = v & "ido"


            End If

            'correcci�n de irregularidades
            If f = "cir" And (Mid$(v, Len(r) + 1, 1) = "a" Or Mid$(v, Len(r) + 1, 1) = "o") Then Mid$(v, Len(r), 1) = "z"
            If f = "gir" And (Mid$(v, Len(r) + 1, 1) = "a" Or Mid$(v, Len(r) + 1, 1) = "o") Then
                Mid$(v, Len(r), 1) = "j"
            End If
            If f = "guir" And (Mid$(v, Len(r) + 1, 1) = "a" Or Mid$(v, Len(r) + 1, 1) = "o") Then Mid$(v, Len(r), 1) = ""
            If f = "quir" And (Mid$(v, Len(r) + 1, 1) = "a" Or Mid$(v, Len(r) + 1, 1) = "o") Then Mid$(v, Len(r) - 1, 2) = "c"

#End Region
        Else 'no se encontr� la conjugaci�n
            v = v2
        End If


        Verbo = v
    End Function

    Sub SepararSufijos(ByRef s As String)

        'separar signos de admiraci�n e interrogaci�n
        s = Replace$(s, "?", " ? ")
        s = Replace$(s, "�", " � ")
        s = Replace$(s, "�", " � ")
        s = Replace$(s, "!", " ! ")
        'separar signos de puntuaci�n
        s = Replace$(s, ".- ", " .- ")
        s = Replace$(s, ". ", " . ")
        s = Replace$(s, ",", " , ")
        s = Replace$(s, "; ", " ; ")
        s = Replace$(s, ": ", " : ")
        s = Replace$(s, "- ", " - ")
        s = Replace$(s, " -", " - ")

    End Sub



End Module
