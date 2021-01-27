Module GramaticaEspañola

    'Esta función determina si se trata de una vocal o una consonante o ninguna de las dos
    Function Letra(ByVal letr As String) As String

        Dim s As String = "xx"
        If letr Like "[aeiouáéíóúü]" Then
            s = "vo" 'vocal
        ElseIf letr Like "[bcdfghjklmnñpqrstvwzyz]" Then
            s = "co" 'consonante
        End If

        Return s
    End Function

    Function SepararSílabas(ByVal palabra As String) As String()
        Dim i As Long
        Dim res As String, silaba As String
        res = palabra

        For i = 1 To Len(palabra)
            silaba = Mid$(palabra, i, 4)
            If silaba Like "[aeiouáéíóúü][!aeiouáéíóúü][aeiouáéíóúü]*" Then
                res = Mid$(res, i, 1) & "/" & Mid$(res, i + 1)
            ElseIf silaba Like "[aeiouáéíóúü][!aeiouáéíóúü][!aeiouáéíóúü][aeiouáéíóúü]" Then
                res = Mid$(res, i, 2) & "/" & Mid$(res, i + 2)
            End If
        Next
        SepararSílabas = Split(res, "/")
    End Function

    'ARTÍCULOS **********************************************************************************
    'Esta función da el artículo correspondiente
    'NOTA 1: el artículo se debuelve en minúsculas.
    Function Artículo(ByVal forma As String) As String
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
        Artículo = res

        'SEP. 2010, OK.
    End Function

    'Esta función da el artículo correspondiente
    'NOTA 1: art debe estar en minúsculas.
    'NOTA 2: si art no es un artículo la función responde "Error".
    Function RECArtículo(ByVal art As String) As String
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

        RECArtículo = res

        'SEP. 2010, OK. Modif. DIC. 2010
    End Function

    '**************************************************************************
    'NOMBRES SUSTANTIVOS **********************************************************************************

    'Esta función da el plural de un nombre
    'todavía no funciona bien: camión --> camiónes

    Function Plural(ByVal nombre As String) As String
        'excepciones
        If nombre = "papá" Or nombre = "mamá" Then
            nombre = nombre & "s"
        ElseIf nombre Like "*s" Then
            'si no son agudos quedan igual <-- falta agregar esa regla
        ElseIf nombre Like "*[aeiouüé]" Then
            nombre = nombre & "s"
        ElseIf nombre Like "*[!aeiouüz]" Then
            nombre = nombre & "es"
        ElseIf nombre Like "*z" Then
            nombre = Mid$(nombre, 1, Len(nombre) - 1) & "ces"
        End If

        Plural = nombre
    End Function
    'Esta función da el plural de una palabra o serie de palabras


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
            ElseIf s Like "*[ns]es" And Not s Like "*[áéíóú]*" Then 'aguda terminada en n o s.
                Dim i As Long
                s = Left(s, Len(s) - 2)


                For i = 1 To Len(s)
                    Select Case Mid$(s, Len(s) - i, 1)
                        Case "a"
                            Mid$(s, Len(s) - i, 1) = "á"
                            Exit For
                        Case "e"
                            Mid$(s, Len(s) - i, 1) = "é"
                            Exit For
                        Case "i"
                            Mid$(s, Len(s) - i, 1) = "í"
                            Exit For
                        Case "o"
                            Mid$(s, Len(s) - i, 1) = "ó"
                            Exit For
                        Case "u"
                            Mid$(s, Len(s) - i, 1) = "ú"
                            Exit For
                    End Select

                Next
            ElseIf s Like "*ores" Or s Like "*ales" Then
                s = Left(s, Len(s) - 2)
            ElseIf s Like "*[aeiouüé]s" Then
                s = Left(s, Len(s) - 1)
            ElseIf s Like "*[!aeiouüz]es" Then
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
        'res = "tú")
        ' End Select

        If forma = "Psn1n" Then
            res = "yo"
        ElseIf forma = "Psn2n" Then
            res = "tú"
        ElseIf forma = "Psm3n" Then
            res = "él"
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
            res = "a mí"
        ElseIf forma = "Ps2a" Then
            res = "a tí"
        ElseIf forma = "Psm3a" Then
            res = "a él"
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
            Case "tú"
                res = "=U"
            Case "él"
                res = "=E"
            Case "élla"
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

            Case "contra"       'anti-orientación
                res = "Pc"
            Case "hacia"        'orientación
                res = "Pi"

                'PARCIALES y otras
            Case "ante", "bajo", "cabe", "sobre", "tras", "entre" 'partes de "en"
                res = "Ps0"

            Case "desde", "hasta"
                res = "P1"

            Case "excepto", "salvo"
                res = "P2"

            Case "según"
                res = "P3"

            Case Else
                res = "Error"

        End Select

        RECPrep = res
    End Function

    Function RECCuan(ByVal prep As String) As String
        Dim res As String
        Select Case prep

            Case "todo", "algún", "ningún", "toda", "alguna", "ninguna", "cada"
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

    'reconoce partícula (conectvas o conexiones de período)
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

        Dim d = ",estúpido,estúpida,tonto,tonta,tarado,tarada,menso,mensa,idiota,tarugo,taruga,mentecato,mentecata,inepto,inepta,imbecil,baboso,babosa"

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
            Case "éramos"
                res = "1pp"
            Case "eran"
                res = "3pp"
            Case "eras"
                res = "2sp"
            Case "será"
                res = "3sf"
            Case "seré"
                res = "1sf"
            Case "serán"
                res = "3pf"
            Case "seremos"
                res = "1pf"
            Case "serás"
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
                res = "4PP" '4 = 2 ó 3.
            Case "estoy"
                res = "e1"
            Case "estás"
                res = "e2"
            Case "está"
                res = "e3"
            Case "estamos"
                res = "e1p"
            Case "están"
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
        FormasVerbales = ",nació,nace,ha nacido,nacerá,corre,corrió,corría,va,vien,fue,ha sido,vió,oyó,escribe,escribió,ha escrito,escribirá,"
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
        FormasVerbales(1) = ",lunes,martes,miércoles,jueves,viernes,sábado,domingo,mañana,ayer,anteayer"
        FormasVerbales(2) = ",minuto,hora,día,semana,mes,año,lustro,sexenio,década,siglo,centuria,milenio,época,tiempo,"
        FormasVerbales(4) = ",minutos,horas,días,semanas,meses,años,lustros,sexenios,décadas,siglos,centurias,milenios,épocas,"
        FormasVerbales(3) = ",primavera,verano,estío,otoño,invierno,"
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

    Function RECNúmero(ByVal expr As String) As String
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
        If flag Then RECNúmero = expr : Exit Function

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
            Case "dieciséis"
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
                res = "1°"
            Case "segundo"
                res = "2°"
            Case "tercero", "tercer"
                res = "3°"
            Case "cuarto"
                res = "4°"
            Case "quinto"
                res = "5°"
            Case "sexto"
                res = "6°"
            Case "séptimo"
                res = "7°"
            Case "octavo"
                res = "8°"
            Case "noveno"
                res = "9°"
            Case "décimo"
                res = "10°"
            Case "undécimo"
                res = "11°"
            Case "duodécimo"
                res = "12°"

            Case "centésimo"
                res = "100°"

            Case "milésimo"
                res = "1000"

            Case Else
                res = "Error"
        End Select


        RECNúmero = res
    End Function

    'VERBOS **********************************************************************************
    'esta función recibe un verbo en forma infinitiva y lo debuevle con la conjugación especificada
    'versión 1.2
    Function Verbo(ByVal v As String, ByVal forma As String) As String
        Dim f As String, r As String, v2 As String
        f = ""
        v2 = v 'en caso de error 

#Region "formas numéricas"
        'para formas numéricas
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
            'prever para corrección de irregularidades
            If v Like "*cer" Then f = "cer"
            If v Like "*eer" Then f = "eer"
            If v Like "*ger" Then f = "ger"

            'obtener raíz del verbo
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
                v = v & "í" 'pasdo
            ElseIf forma = "r1p" Then
                v = v & "imos" 'pasdo
            ElseIf forma = "r2s" Then
                v = v & "iste" 'pasdo
            ElseIf forma = "r2p" Then
                v = v & "ieron" 'pasdo
            ElseIf forma = "r3s" Then
                v = v & "ió" 'pasdo
            ElseIf forma = "r3p" Then
                v = v & "ieron" 'pasado

                'futuro
            ElseIf forma = "f1s" Then
                v = v & "eré" 'futuro
            ElseIf forma = "f1p" Then
                v = v & "erémos" 'futuro
            ElseIf forma = "f2s" Then
                v = v & "erás" 'futuro
            ElseIf forma = "f2p" Then
                v = v & "erán" 'futuro
            ElseIf forma = "f3s" Then
                v = v & "erá" 'futuro
            ElseIf forma = "f3p" Then
                v = v & "erán" 'futuro

                'copretérito
            ElseIf forma = "c1s" Then
                v = v & "ía"
            ElseIf forma = "c1p" Then
                v = v & "íamos"
            ElseIf forma = "c2s" Then
                v = v & "ías"
            ElseIf forma = "c2p" Then
                v = v & "ían"
            ElseIf forma = "c3s" Then
                v = v & "ía"
            ElseIf forma = "c3p" Then
                v = v & "ían"
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
                'antepretérito
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
                v = "habré " & v & "ido"
            ElseIf forma = "u1p" Then
                v = "habremos " & v & "ido"
            ElseIf forma = "u2s" Then
                v = "habrás " & v & "ido"
            ElseIf forma = "u2p" Then
                v = "habrán " & v & "ido"
            ElseIf forma = "u3s" Then
                v = "habrá " & v & "ido"
            ElseIf forma = "u3p" Then
                v = "habrán " & v & "ido"
                'antecopretérito
            ElseIf forma = "v1s" Then
                v = "había " & v & "ido"
            ElseIf forma = "v1p" Then
                v = "haíamos " & v & "ido"
            ElseIf forma = "v2s" Then
                v = "habías " & v & "ido"
            ElseIf forma = "v2p" Then
                v = "habían " & v & "ido"
            ElseIf forma = "v3s" Then
                v = "había " & v & "ido"
            ElseIf forma = "v3p" Then
                v = "habían " & v & "ido"

                'MODO POTENCIAL
                'simple
            ElseIf forma = "Ps1s" Then
                v = v & "ería"
            ElseIf forma = "Ps1p" Then
                v = v & "eríamos"
            ElseIf forma = "Ps2s" Then
                v = v & "erías"
            ElseIf forma = "Ps2p" Then
                v = v & "erían"
            ElseIf forma = "Ps3s" Then
                v = v & "ería"
            ElseIf forma = "Ps1p" Then
                v = v & "erían"
                'compuesto
            ElseIf forma = "Pc1s" Then
                v = "habría " & v & "ido"
            ElseIf forma = "Pc1p" Then
                v = "habríamos " & v & "ido"
            ElseIf forma = "Pc2s" Then
                v = "habrías " & v & "ido"
            ElseIf forma = "Pc2p" Then
                v = "habrían " & v & "ido"
            ElseIf forma = "Pc3s" Then
                v = "habría " & v & "ido"
            ElseIf forma = "Pc3p" Then
                v = "habrían " & v & "ido"
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
                'pretérito
                '1era forma
            ElseIf forma = "Sr1s" Then
                v = v & "iera"
            ElseIf forma = "Sr1p" Then
                v = v & "iéramos"
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
                v = v & "iésemos"
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
                v = v & "iéremos"
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
                'antepretérito
                '1era forma
            ElseIf forma = "Sb1s" Then
                v = "hubiera " & v & "ido"
            ElseIf forma = "Sb1p" Then
                v = "hubiéramos " & v & "ido"
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
                v = "hubiésemos " & v & "ido"
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
                v = "hubiéremos " & v & "ido"
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
            If f = "eer" And v Like "*[aeiouáéíóúü]i[aeiouáéíóúü]*" Then v = Replace(v, "i", "y") '<-- falta afinar


#End Region

#Region "Verbos terminados en -ar"
            '----------------------------------------------------------------
        ElseIf v Like "*?ar" Then

            'prever para corrección de irregularidades
            If v Like "*car" Then f = "car"
            If v Like "*gar" Then f = "gar"
            If v Like "*zar" Then f = "zar"

            'obtener raíz
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
            ElseIf forma = "p2s" Then 'tú
                v = v & "as" 'presente
            ElseIf forma = "p2p" Then 'ustedes
                v = v & "an" 'presente
            ElseIf forma = "p3s" Then 'él/ella
                v = v & "a" 'presente
            ElseIf forma = "p3p" Then 'ellos/ellas
                v = v & "an" 'presente

                'pasado
            ElseIf forma = "r1s" Then
                v = v & "í" 'pasdo
            ElseIf forma = "r1p" Then
                v = v & "imos" 'pasdo
            ElseIf forma = "r2s" Then
                v = v & "iste" 'pasdo
            ElseIf forma = "r2p" Then
                v = v & "ieron" 'pasdo
            ElseIf forma = "r3s" Then
                v = v & "ió" 'pasdo
            ElseIf forma = "r3p" Then
                v = v & "ieron" 'pasado

                'futuro
            ElseIf forma = "f1s" Then
                v = v & "eré" 'futuro
            ElseIf forma = "f1p" Then
                v = v & "erémos" 'futuro
            ElseIf forma = "f2s" Then
                v = v & "erás" 'futuro
            ElseIf forma = "f2p" Then
                v = v & "erán" 'futuro
            ElseIf forma = "f3s" Then
                v = v & "erá" 'futuro
            ElseIf forma = "f3p" Then
                v = v & "erán" 'futuro

                'copretérito
            ElseIf forma = "c1s" Then
                v = v & "ía"
            ElseIf forma = "c1p" Then
                v = v & "íamos"
            ElseIf forma = "c2s" Then
                v = v & "ías"
            ElseIf forma = "c2p" Then
                v = v & "ían"
            ElseIf forma = "c3s" Then
                v = v & "ía"
            ElseIf forma = "c3p" Then
                v = v & "ían"
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
                'antepretérito
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
                v = "habré " & v & "ido"
            ElseIf forma = "u1p" Then
                v = "habremos " & v & "ido"
            ElseIf forma = "u2s" Then
                v = "habrás " & v & "ido"
            ElseIf forma = "u2p" Then
                v = "habrán " & v & "ido"
            ElseIf forma = "u3s" Then
                v = "habrá " & v & "ido"
            ElseIf forma = "u3p" Then
                v = "habrán " & v & "ido"
                'antecopretérito
            ElseIf forma = "v1s" Then
                v = "había " & v & "ido"
            ElseIf forma = "v1p" Then
                v = "haíamos " & v & "ido"
            ElseIf forma = "v2s" Then
                v = "habías " & v & "ido"
            ElseIf forma = "v2p" Then
                v = "habían " & v & "ido"
            ElseIf forma = "v3s" Then
                v = "había " & v & "ido"
            ElseIf forma = "v3p" Then
                v = "habían " & v & "ido"

                'MODO POTENCIAL
                'simple
            ElseIf forma = "Ps1s" Then
                v = v & "ería"
            ElseIf forma = "Ps1p" Then
                v = v & "eríamos"
            ElseIf forma = "Ps2s" Then
                v = v & "erías"
            ElseIf forma = "Ps2p" Then
                v = v & "erían"
            ElseIf forma = "Ps3s" Then
                v = v & "ería"
            ElseIf forma = "Ps1p" Then
                v = v & "erían"
                'compuesto
            ElseIf forma = "Pc1s" Then
                v = "habría " & v & "ido"
            ElseIf forma = "Pc1p" Then
                v = "habríamos " & v & "ido"
            ElseIf forma = "Pc2s" Then
                v = "habrías " & v & "ido"
            ElseIf forma = "Pc2p" Then
                v = "habrían " & v & "ido"
            ElseIf forma = "Pc3s" Then
                v = "habría " & v & "ido"
            ElseIf forma = "Pc3p" Then
                v = "habrían " & v & "ido"
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
                'pretérito
                '1era forma
            ElseIf forma = "Sr1s" Then
                v = v & "iera"
            ElseIf forma = "Sr1p" Then
                v = v & "iéramos"
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
                v = v & "iésemos"
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
                v = v & "iéremos"
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
                'antepretérito
                '1era forma
            ElseIf forma = "Sb1s" Then
                v = "hubiera " & v & "ido"
            ElseIf forma = "Sb1p" Then
                v = "hubiéramos " & v & "ido"
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
                v = "hubiésemos " & v & "ido"
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
                v = "hubiéremos " & v & "ido"
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
            'prever para corrección de irregularidades
            If v Like "*cir" Then f = "cir"
            If v Like "*gir" Then f = "gir"
            If v Like "*guir" Then f = "guir"
            If v Like "*quir" Then f = "quir"

            'obtener raíz
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
                v = v & "í" 'pasdo
            ElseIf forma = "r1p" Then
                v = v & "imos" 'pasdo
            ElseIf forma = "r2s" Then
                v = v & "iste" 'pasdo
            ElseIf forma = "r2p" Then
                v = v & "ieron" 'pasdo
            ElseIf forma = "r3s" Then
                v = v & "ió" 'pasdo
            ElseIf forma = "r3p" Then
                v = v & "ieron" 'pasado

                'futuro
            ElseIf forma = "f1s" Then
                v = v & "iré" 'futuro
            ElseIf forma = "f1p" Then
                v = v & "irémos" 'futuro
            ElseIf forma = "f2s" Then
                v = v & "irás" 'futuro
            ElseIf forma = "f2p" Then
                v = v & "irán" 'futuro
            ElseIf forma = "f3s" Then
                v = v & "irá" 'futuro
            ElseIf forma = "f3p" Then
                v = v & "irán" 'futuro

                'copretérito
            ElseIf forma = "c1s" Then
                v = v & "ía"
            ElseIf forma = "c1p" Then
                v = v & "íamos"
            ElseIf forma = "c2s" Then
                v = v & "ías"
            ElseIf forma = "c2p" Then
                v = v & "ían"
            ElseIf forma = "c3s" Then
                v = v & "ía"
            ElseIf forma = "c3p" Then
                v = v & "ían"
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
                'antepretérito
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
                v = "habré " & v & "ido"
            ElseIf forma = "u1p" Then
                v = "habremos " & v & "ido"
            ElseIf forma = "u2s" Then
                v = "habrás " & v & "ido"
            ElseIf forma = "u2p" Then
                v = "habrán " & v & "ido"
            ElseIf forma = "u3s" Then
                v = "habrá " & v & "ido"
            ElseIf forma = "u3p" Then
                v = "habrán " & v & "ido"
                'antecopretérito
            ElseIf forma = "v1s" Then
                v = "había " & v & "ido"
            ElseIf forma = "v1p" Then
                v = "haíamos " & v & "ido"
            ElseIf forma = "v2s" Then
                v = "habías " & v & "ido"
            ElseIf forma = "v2p" Then
                v = "habían " & v & "ido"
            ElseIf forma = "v3s" Then
                v = "había " & v & "ido"
            ElseIf forma = "v3p" Then
                v = "habían " & v & "ido"

                'MODO POTENCIAL
                'simple
            ElseIf forma = "Ps1s" Then
                v = v & "iría"
            ElseIf forma = "Ps1p" Then
                v = v & "iríamos"
            ElseIf forma = "Ps2s" Then
                v = v & "irías"
            ElseIf forma = "Ps2p" Then
                v = v & "irían"
            ElseIf forma = "Ps3s" Then
                v = v & "iría"
            ElseIf forma = "Ps1p" Then
                v = v & "irían"
                'compuesto
            ElseIf forma = "Pc1s" Then
                v = "habría " & v & "ido"
            ElseIf forma = "Pc1p" Then
                v = "habríamos " & v & "ido"
            ElseIf forma = "Pc2s" Then
                v = "habrías " & v & "ido"
            ElseIf forma = "Pc2p" Then
                v = "habrían " & v & "ido"
            ElseIf forma = "Pc3s" Then
                v = "habría " & v & "ido"
            ElseIf forma = "Pc3p" Then
                v = "habrían " & v & "ido"
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
                'pretérito
                '1era forma
            ElseIf forma = "Sr1s" Then
                v = v & "iera"
            ElseIf forma = "Sr1p" Then
                v = v & "iéramos"
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
                v = v & "iésemos"
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
                v = v & "iéremos"
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
                'antepretérito
                '1era forma
            ElseIf forma = "Sb1s" Then
                v = "hubiera " & v & "ido"
            ElseIf forma = "Sb1p" Then
                v = "hubiéramos " & v & "ido"
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
                v = "hubiésemos " & v & "ido"
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
                v = "hubiéremos " & v & "ido"
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

            'corrección de irregularidades
            If f = "cir" And (Mid$(v, Len(r) + 1, 1) = "a" Or Mid$(v, Len(r) + 1, 1) = "o") Then Mid$(v, Len(r), 1) = "z"
            If f = "gir" And (Mid$(v, Len(r) + 1, 1) = "a" Or Mid$(v, Len(r) + 1, 1) = "o") Then
                Mid$(v, Len(r), 1) = "j"
            End If
            If f = "guir" And (Mid$(v, Len(r) + 1, 1) = "a" Or Mid$(v, Len(r) + 1, 1) = "o") Then Mid$(v, Len(r), 1) = ""
            If f = "quir" And (Mid$(v, Len(r) + 1, 1) = "a" Or Mid$(v, Len(r) + 1, 1) = "o") Then Mid$(v, Len(r) - 1, 2) = "c"

#End Region
        Else 'no se encontró la conjugación
            v = v2
        End If


        Verbo = v
    End Function

    Sub SepararSufijos(ByRef s As String)

        'separar signos de admiración e interrogación
        s = Replace$(s, "?", " ? ")
        s = Replace$(s, "¿", " ¿ ")
        s = Replace$(s, "¡", " ¡ ")
        s = Replace$(s, "!", " ! ")
        'separar signos de puntuación
        s = Replace$(s, ".- ", " .- ")
        s = Replace$(s, ". ", " . ")
        s = Replace$(s, ",", " , ")
        s = Replace$(s, "; ", " ; ")
        s = Replace$(s, ": ", " : ")
        s = Replace$(s, "- ", " - ")
        s = Replace$(s, " -", " - ")

    End Sub



End Module
