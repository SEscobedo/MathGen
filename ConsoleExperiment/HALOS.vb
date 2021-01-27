Imports System.IO
Module MdHALOS

    Public datos As New PersonalDATA 'perfil del usuario

#Region "Estructura de clases"

    '********************************
    'CLASES
    '********************************
    Class PersonalDATA
        Property perfiles As New List(Of Perfil)
        Property contactos As New List(Of Contacto)
        Property lugares As New List(Of Mundo.Lugar)
    End Class

    Class objeto
        Property pensable As Boolean
        Property real As Boolean
    End Class

    Class hecho
        Property tiempo As Date
    End Class

    Class historia

    End Class

    Class tiempo
        Property Fechainicio As New Date
        Property FechaFin As New Date
        Property duracion As New TimeSpan
        Property horaInicio As New Date
        Property horaFin As New Date
        Property Patron As String
        'Property CondInicio As String
        'Property Condfin As String
        'Property CondDuracion As String
        Function FromString(s As String) As tiempo
            Dim t As New tiempo
            Dim a = Split(s, ",")
            If Not a(0) = "" Then t.Fechainicio = Date.Parse(a(0))
            If UBound(a) > 0 And Not a(1) = "" Then t.FechaFin = Date.Parse(a(1))
            If UBound(a) > 1 And Not a(2) = "" Then t.duracion = TimeSpan.Parse(a(2))
            If UBound(a) > 2 And Not a(3) = "" Then t.horaInicio = Date.Parse(a(3))
            If UBound(a) > 3 And Not a(4) = "" Then t.horaFin = Date.Parse(a(4))
            If UBound(a) > 4 And Not a(5) = "" Then t.Patron = a(5)
            Return t
        End Function

    End Class

    Class Mundo
        Property Objetos As List(Of objeto)
        Property Hechos As List(Of hecho)
        Class Tierra
            Inherits objeto
        End Class
        Class Lugar

            Class Residencia
                Property calle As String
                Property número As String
                Property númeroInterior As String
                Property colonia As String
                Property códigoPostal As String
                Property Municiopio As String
            End Class
        End Class
        Class Terreno
            Property Fronteras As Double()

        End Class
    End Class

    Class Perfil 'Persona capaz de intencionalidad
        Inherits objeto
        Property Proyectos As New List(Of proyect)
        Property Voliciones As New List(Of String)
        Property Descripción As New DescripcionPefil
        Property historial As New historia
        Property contactos As New List(Of Contacto)
        Property Recursos As New RecursosPersona
        Property Control As New ControlDeProcesos

    End Class

    Class ControlDeProcesos
        Property Calendario As List(Of Evento)
    End Class

    Class RecursosPersona
        Property Economia As String
        Property tiempo As String
        Property habilidades As String
        Property conocimiento As String
    End Class

    Class DescripcionPefil
        Property Nombre As String
        Property FechaNacimiento As Date
        Property telefonoCasa As Long
        Property telefonoMovil As Long
        Property Correo As String
        Property Direccion As String
        Property Sexo As String
    End Class

    Class proyect
        Property Subproyectos As New List(Of proyect)
        Property Tareas As New List(Of tarea)
        Property CriteriosDeCumplimiento As String
        Property CostoProyecto As New costo
        'Property Prerequisitos As string
        Property RecursosDisponibles As RecursosProyecto
        Property Descripción As New DescripcionProyecto
        Property Archivos As Path
        Property colaboradores As String
    End Class

    Class RecursosProyecto
        Property Conctactos As List(Of Contacto) 'REFERENCIA AL INDICE DE CONTACTOS
        Property Institución As Institución

    End Class

    Class Institución
        Property Descripcion As DescripcionPefil
    End Class

    Class Universidad
        Inherits Institución

    End Class

    Class tarea

        Property Título As String
        Property Tipotarea As String
        Property Esquema As String

        'Estas propiedades pueden ser: Determinadas/indeterminadas; Quantivas/Qualitativas; Condicionales/Absolutas. [Planeadas-hechas]
        Property lugar As String
        Property tiempo As New tiempo
        Property circunstancias As String

        Property Estado As String '¿En que estado se encuentra la realización de la tarea? ¿terminada? ¿por realizar?
        Property Evaluacion As String '¿Cuál fue el resultado?
        Property Costo As costo '¿Cuantos recursos gastaremos en la tarea?
        Property Prerequisitos As String '¿Qué necesitamos para poder realizar la tarea?

    End Class
    Class costo
        Property CostoDeTiempo As tiempo
        Property CostoMonetario As String
        Property CostoRecursos As String
    End Class

    Class DescripcionProyecto
        Property Id As Long
        Property Tipo As String
        Property Clave As String
        Property Nombre As String
    End Class

    Class Contacto
        Property descripcion As DescripcionPefil
    End Class

    Class colaborador
        Property ID As Long 'REFERENCIA A LA LISTA CENTRAL DE CONTACTOS
        Property tipoColaboracion As String
    End Class

    Class Evento
        Property inicio As Date
        Property fin As Date
        Property lugar As String
        Property Modo As String 'Necesario, opcional, conveniente, condicional'
        Property circunstancias As String
        Property estado As Integer
    End Class
#End Region

#Region "Carga de estructuras nativas"
    '::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    'CARGA DE ESTRUCTRUAS NATIVAS
    '::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    'PERFIL DEL ADMINISTRADOR
    Public Admin As New Perfil

    'PROYECTO DE VIDA
    Dim Vita As New proyect
    'PROYECTO UNIVERSITARIO
    Dim Carrera As New proyect

    Sub CargarEstructura()
        Vita.Descripción.Nombre = "Mantener la vida"

        'Agregar el proyecto fundamental
        Admin.Proyectos.Add(Vita)
    End Sub

    Sub CrearUniversidad()
        Dim UDG As New Universidad

        Carrera.Descripción.Tipo = "Licenciatura"
        Carrera.RecursosDisponibles.Institución = UDG

        UDG.Descripcion.Nombre = "Universidad de Guadalajara"


        'Crear materias
        For i = 1 To 30
            Dim materia As New proyect
            materia.Descripción.Tipo = "asignatura académica"
            Carrera.Subproyectos.Add(materia)
        Next

        Carrera.Subproyectos(0).Descripción.Nombre = "Física Moderna"
        Carrera.Subproyectos(1).Descripción.Nombre = "Taller de Física Moderna"
        Carrera.Subproyectos(2).Descripción.Nombre = "Electromagnetismo"

        Dim clase As New tarea
        clase.Título = "Física Moderna"
        clase.Tipotarea = "Aula"
        'clase.tiempo = "07:00-08:55;L,M;17/08/2015-17/12/2015"

        'clase.tiempo.inicio = Date.Parse("7:00")
        clase.lugar = "\UDG\V2\s95"
        clase.Estado = "No comenzado"
        Carrera.Subproyectos(0).Tareas.Add(clase)


    End Sub

#End Region

#Region "Carga de estructuras y datos"

    Sub CargarEstructuras()
        Try
            Dim File1 As New System.IO.StreamReader("datos.txt")
            Dim LF = File1.ReadToEnd
            File1.Close()
            Dim L = Split(LF, vbCrLf)

            'compilar datos
            For i = 0 To UBound(L)
                L(i) = Trim(L(i))
            Next
            datos = New PersonalDATA
            CMrk(L, datos)

        Catch ex As Exception
            Console.WriteLine(Err.Description)
        End Try

    End Sub

    Dim tar As tarea
    Dim desc As DescripcionProyecto

    Sub CMrk(ByRef T As String(), ByRef d As PersonalDATA)

        Static i As Long = 0
        Static proy As Long = 0
        Static q As Long = -1
        Static Nperfil As Long = 0
        Static Nproyecto As Long = 0
        Static Nsubproyecto As Long = 0
        Static Nsub As Long = 0


        If T(i) = "fin documento" Then
            Exit Sub
        ElseIf T(i) Like "%*" Then
            i += 1
            CMrk(T, d)
        End If

        If q = -1 And T(i) = "documento" Then
            q = 0
            i += 1
            CMrk(T, d)

        ElseIf q = 0 Then 'documento ================================================

            If T(i) = "perfil" Then
                Dim per As New Perfil
                d.perfiles.Add(per)
                Nperfil = d.perfiles.Count - 1
                q = 1
                i += 1
            ElseIf T(i) = "contacto" Then
                Dim con As New Contacto
                d.contactos.Add(con)
                q = 3
                i += 1
            ElseIf T(i) = "recurso" Then

                q = 4
                i += 1
            End If

        ElseIf q = 1 Then 'Perfil'===================================================
            If T(i) = "proyecto" Then
                q = 101
                i += 1
                Dim p As New proyect
                d.perfiles(Nperfil).Proyectos.Add(p)
                Nproyecto = d.perfiles(Nperfil).Proyectos.Count - 1
                CMrk(T, d)
            ElseIf T(i) = "descripcion" Then
                q = 102
                i += 1
                'Dim DescP As New DescripcionPefil
                'd.perfiles(Nperfil).Descripción 
                CMrk(T, d)
            ElseIf T(i) = "fin perfil" Then
                q = 0
                i += 1
                CMrk(T, d)
            End If
        ElseIf q = 101 Then 'Proyecto '=====================================================
            If T(i) Like "cirterios de cumplimiento=*" Then
                d.perfiles(Nperfil).Proyectos(Nproyecto).CriteriosDeCumplimiento = Mid(T(i), Len("cirterios de cumplimiento="))
                i += 1
                CMrk(T, d)
            ElseIf T(i) Like "colaboradores=*" Then
                d.perfiles(Nperfil).Proyectos(Nproyecto).colaboradores = Mid(T(i), Len("colaboradores="))
                i += 1
                CMrk(T, d)
            ElseIf T(i) = "subproyecto" Then
                q = 101
                i += 1
                Nsub += 1
                Dim p As New proyect
                d.perfiles(Nperfil).Proyectos(Nproyecto).Subproyectos.Add(p)
                Nsubproyecto = d.perfiles(Nperfil).Proyectos(Nproyecto).Subproyectos.Count - 1
                CMrk(T, d)

            ElseIf T(i) = "descripcion" Then
                i += 1
                q = 10101
                desc = New DescripcionProyecto
                CMrk(T, d)
            ElseIf T(i) = "tarea" Then
                i += 1
                q = 10102
                tar = New tarea
                If Nsub = 0 Then
                    d.perfiles(Nperfil).Proyectos(Nproyecto).Tareas.Add(tar)
                ElseIf Nsub = 1 Then
                    d.perfiles(Nperfil).Proyectos(Nproyecto).Subproyectos(Nsubproyecto).Tareas.Add(tar)
                End If
                CMrk(T, d)

            ElseIf T(i) = "fin proyecto" Then
                q = 1
                i += 1
                CMrk(T, d)
            ElseIf T(i) = "fin subproyecto" Then
                q = 101
                i += 1
                Nsub -= 1
                CMrk(T, d)
            End If
        ElseIf q = 102 Then 'Descripcion Perfil' =============================================

            If T(i) Like "e-mail=*" Then
                d.perfiles(Nperfil).Descripción.Correo = Mid(T(i), Len("e-mail=*"))
                i += 1
                CMrk(T, d)
            ElseIf T(i) Like "direccion=*" Then
                d.perfiles(Nperfil).Descripción.Direccion = Mid(T(i), Len("direccion=*"))
                i += 1
                CMrk(T, d)
            ElseIf T(i) Like "fecha_nacimiento=*" Then
                d.perfiles(Nperfil).Descripción.FechaNacimiento = Mid(T(i), Len("fecha_nacimiento=*"))
                i += 1
                CMrk(T, d)
            ElseIf T(i) Like "nombre=*" Then
                d.perfiles(Nperfil).Descripción.Nombre = Mid(T(i), Len("nombre=*"))
                i += 1
                CMrk(T, d)
            ElseIf T(i) Like "sexo=*" Then
                d.perfiles(Nperfil).Descripción.Sexo = Mid(T(i), Len("sexo=*"))
                i += 1
                CMrk(T, d)
            ElseIf T(i) Like "telefono_fijo=*" Then
                d.perfiles(Nperfil).Descripción.telefonoCasa = Mid(T(i), Len("telefono_fijo=*"))
                i += 1
                CMrk(T, d)
            ElseIf T(i) Like "movil=*" Then
                d.perfiles(Nperfil).Descripción.telefonoMovil = Mid(T(i), Len("movil=*"))
                i += 1
                CMrk(T, d)
            ElseIf T(i) Like "fin descripcion" Then
                q = 1
                i += 1
                CMrk(T, d)
            End If

        ElseIf q = 10101 Then 'Descripcion Proyecto' =============================================

            If T(i) Like "tipo=*" Then
                desc.Tipo = Mid(T(i), Len("tipo=*"))
                i += 1
                CMrk(T, d)
            ElseIf T(i) Like "clave=*" Then

                desc.Clave = Mid(T(i), Len("clave=*"))
                i += 1
                CMrk(T, d)
            ElseIf T(i) Like "id=*" Then

                desc.Id = Val(Mid(T(i), Len("id=*")))
                i += 1
                CMrk(T, d)
            ElseIf T(i) Like "nombre=*" Then

                desc.Nombre = Mid(T(i), Len("nombre=*"))
                i += 1
                CMrk(T, d)

                '  ElseIf T(i) = "colaborador" Then
                'Dim col As New colaborador
                'd.perfiles(Nperfil).Proyectos(Nproyecto).Descripción.Colaboradores.Add(col)
                'i += 1
                'CMrk(T, d)
            ElseIf T(i) = "fin descripcion" Then
                If Nsub = 0 Then
                    d.perfiles(Nperfil).Proyectos(Nproyecto).Descripción = desc
                ElseIf Nsub = 1 Then
                    d.perfiles(Nperfil).Proyectos(Nproyecto).Subproyectos(Nsubproyecto).Descripción = desc
                End If
                q = 101
                i += 1
                CMrk(T, d)
            End If

            'ElseIf q = 1010101 Then 'colaborador =========================================================
            '    Dim col As New colaborador
            '    If T(i) Like "contacto=*" Then
            '        col.ID = Mid(T(i), Len("contacto=*"))
            '        i += 1
            '        CMrk(T, d)
            '    ElseIf T(i) Like "gestion=*" Then
            '        col.tipoColaboracion = Mid(T(i), Len("gestion=*"))
            '        i += 1
            '        CMrk(T, d)
            '    ElseIf T(i) Like "fin colaborador" Then
            '        q = 10101
            '        i += 1

            '        CMrk(T, d)
            '    End If

        ElseIf q = 10102 Then 'Tarea =========================================================

            If T(i) Like "evaluacion=*" Then
                tar.Evaluacion = Mid(T(i), Len("evaluacion=*"))
                i += 1

                CMrk(T, d)
            ElseIf T(i) Like "circunstancias=*" Then

                tar.circunstancias = Mid(T(i), Len("circunstancias=*"))
                i += 1
                CMrk(T, d)

            ElseIf T(i) Like "esquema=*" Then
                tar.Esquema = Mid(T(i), Len("esquema=*"))
                i += 1
                CMrk(T, d)
            ElseIf T(i) Like "estado=*" Then
                tar.Estado = Mid(T(i), Len("estado=*"))
                i += 1
                CMrk(T, d)
            ElseIf T(i) Like "lugar=*" Then
                tar.lugar = Mid(T(i), Len("lugar=*"))
                i += 1
                CMrk(T, d)
            ElseIf T(i) Like "prerequisitos=*" Then
                tar.Prerequisitos = Mid(T(i), Len("prerequisitos=*"))
                i += 1
                CMrk(T, d)
            ElseIf T(i) Like "tiempo=*" Then
                tar.tiempo = tar.tiempo.FromString(Mid(T(i), Len("tiempo=*")))
                i += 1
                CMrk(T, d)
            ElseIf T(i) Like "tipo=*" Then
                tar.Tipotarea = Mid(T(i), Len("tipo=*"))
                i += 1
                CMrk(T, d)
            ElseIf T(i) Like "titulo=*" Then
                tar.Título = Mid(T(i), Len("titulo=*"))
                i += 1
                CMrk(T, d)
                'ElseIf T(i) = "costo" Then
                '    i += 1
                '    q = 101003
                '    tar.Costo = New costo
                '    CMrk(T, d)
            ElseIf T(i) = "fin tarea" Then
                q = 101
                i += 1

                If Nsub = 0 Then
                    d.perfiles(Nperfil).Proyectos(Nproyecto).Tareas(d.perfiles(Nperfil).Proyectos(Nproyecto).Tareas.Count - 1) = tar
                ElseIf Nsub = 1 Then
                    d.perfiles(Nperfil).Proyectos(Nproyecto).Subproyectos(Nsubproyecto).Tareas(d.perfiles(Nperfil).Proyectos(Nproyecto).Subproyectos(Nsubproyecto).Tareas.Count - 1) = tar
                End If
                CMrk(T, d)
            End If

            'ElseIf q = 10103 Then 'costoTarea =========================================================
            '    If T(i) Like "costodinero=*" Then
            '        tar.Evaluacion = Mid(T(i), Len("costodinero=*"))
            '        i += 1
            '        CMrk(T, d)
            '    ElseIf T(i) Like "fin costo" Then

            '    End If
        End If


    End Sub

#End Region

#Region "Procesamineto de datos e IA"

    ':::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    'FUNCIONES DE SEGUMIENTO
    ':::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    Function CalcularCosto(p As proyect) As costo 'Realcula el costo bruto de un proyecto a partir del costo de sus tareas
        Dim i As Long
        'Sumar el costo de las tareas independientes
        For i = 0 To p.Tareas.Count - 1
            p.CostoProyecto.CostoMonetario += p.Tareas(i).Costo.CostoMonetario
        Next

        'Sumar el costo de las tareas independientes
        For i = 0 To p.Subproyectos.Count - 1
            p.CostoProyecto.CostoMonetario += CalcularCosto(p.Subproyectos(i)).CostoMonetario
        Next

    End Function

    Function TareasEsteDia(ByRef p As Perfil, dia As Date) As List(Of tarea) 'Establece las tareas para realizar un día determinado [acepta solo nivel de proyecto-subproyecto]
        Dim res As New List(Of tarea)

        For i = 0 To p.Proyectos.Count - 1
            For j = 0 To p.Proyectos(i).Tareas.Count - 1
                If EsteDia(p.Proyectos(i).Tareas(j), dia) Then
                    res.Add(p.Proyectos(i).Tareas(j))
                End If
            Next
            For k = 0 To p.Proyectos(i).Subproyectos.Count - 1
                For j = 0 To p.Proyectos(i).Subproyectos(k).Tareas.Count - 1
                    If EsteDia(p.Proyectos(i).Subproyectos(k).Tareas(j), dia) Then
                        res.Add(p.Proyectos(i).Subproyectos(k).Tareas(j))
                    End If
                Next
            Next
        Next

        Return res
    End Function

    Function EsteDia(t As tarea, dia As Date) As Boolean 'Determina si una tarea dada se debe realizar el día determinado.
        Dim res As Boolean = False
        Dim di, df As Date

        di = t.tiempo.Fechainicio
        df = t.tiempo.FechaFin

        If di.Date > dia.Date Or df.Date < dia.Date Then
            Return False
            Exit Function
        End If


        If Not t.tiempo.Patron = "" Then
            If t.tiempo.Patron Like "D-*" Then 'diario************************************************
                res = True
            ElseIf t.tiempo.Patron Like "S-*" Then 'semanal*******************************************
                Dim b = Split(Mid(t.tiempo.Patron, 3), "")
                For i = 0 To UBound(b)
                    If dia.DayOfWeek = DiaSemana(b(i)) Then
                        res = True
                        Exit For
                    End If
                Next
            ElseIf t.tiempo.Patron Like "M-*" Then 'mensual*******************************************
                Dim b = Split(Mid(t.tiempo.Patron, 3), ".")
                For i = 0 To UBound(b)
                    If dia.Day = b(i) Then
                        res = True
                        Exit For
                    End If
                Next
            ElseIf t.tiempo.Patron Like "A-*" Then 'anual*********************************************
                Dim b = Split(Mid(t.tiempo.Patron, 3), ".")
                For i = 0 To UBound(b)
                    If dia.Day = b(i) Then
                        res = True
                        Exit For
                    End If
                Next
            End If
        Else 'Caso sin repeticion
            If di.Date <= dia.Date And df >= dia.Date Then res = True
        End If


        Return res
    End Function

    Function DiaSemana(s As String) As Integer 'Función auxiliar para decodificar la variable de tiempo
        Dim res As Integer
        s = LCase(s)
        If s = "l" Then
            res = DayOfWeek.Monday
        ElseIf s = "m" Then
            res = DayOfWeek.Tuesday
        ElseIf s = "i" Then
            res = DayOfWeek.Wednesday
        ElseIf s = "j" Then
            res = DayOfWeek.Thursday
        ElseIf s = "v" Then
            res = DayOfWeek.Friday
        ElseIf s = "s" Then
            res = DayOfWeek.Saturday
        ElseIf s = "d" Then
            res = DayOfWeek.Sunday
        Else
            res = -1
        End If

        Return res
    End Function

#Region "Optimación"

    Function Conflicto(t1 As tarea, t2 As tarea) As String 'Determina si hay conflictos de horario entre dos tareas.
        Dim res As String = ""






        If t1.tiempo.Patron = "" And t2.tiempo.Patron = "" Then 'interseccion de patrones


        ElseIf t1.tiempo.Patron = "" Then
        ElseIf t2.tiempo.Patron = "" Then

        Else

        End If


    End Function

#End Region

#Region "Seguimiento"

#End Region

#Region "Evaluación"

    Function EvaluarProporciones(p As Perfil, t As TimeSpan) As String 'Estima los porcentajes en las proporciones de las diferentes actividades de un perfil

    End Function

#End Region

#Region "Entrada y salida de información"

    Sub HALOS()
        Console.WriteLine("Experimental HALOS prototype.")
        Console.WriteLine("Vers. 1.0")
        Console.WriteLine("Salvador D. Escobedo")
        Console.WriteLine("")
        Console.Write("--:")
        Dim R As String
        Dim res As String

        ' Dim i As Long

        Do
            R = Console.ReadLine()
            R = Trim(R)


            If R = "read data" Then
                CargarEstructuras()

                If Not datos.contactos Is Nothing Then Console.Write("contactos: " & datos.contactos.Count & vbLf)
                If Not datos.perfiles Is Nothing Then Console.Write("perfiles: " & datos.perfiles.Count & vbLf)
                If Not datos.lugares Is Nothing Then Console.Write("lugares: " & datos.lugares.Count & vbLf)

            ElseIf R = "proyects" Then
                Dim p As Perfil = datos.perfiles(0)

                For Each proyect In p.Proyectos
                    Console.WriteLine(proyect.Descripción.Nombre)
                    Console.WriteLine(proyect.Descripción.Id)
                    Console.WriteLine(proyect.Descripción.Tipo)
                    Console.WriteLine(proyect.Descripción.Clave)
                Next
            ElseIf R = "for today" Then
                Dim p As Perfil = datos.perfiles(0)
                Dim L = TareasEsteDia(p, Date.Today)

                For Each T As tarea In L
                    Console.WriteLine(T.Título)
                    Console.WriteLine(T.Tipotarea)
                    Console.WriteLine(T.lugar)
                    Console.WriteLine(T.tiempo)
                    Console.WriteLine("")
                Next

            End If



            Console.WriteLine("")
            Console.Write("--:")

        Loop Until R = "close"

        Console.WriteLine("HALOS closed.")
    End Sub
#End Region

#End Region

End Module
