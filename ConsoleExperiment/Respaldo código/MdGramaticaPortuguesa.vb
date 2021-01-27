Module MdGramaticaPortuguesa

    Function PluralPor(ByVal nombre As String) As String

        If Not nombre Like "*-*" Then
            'Obs.: numerais substantivados terminados em -s ou -z não variam no plural.
            'Por exemplo
            'Nas provas mensais consegui muitos seis e alguns dez.

            'Plural dos Diminutivos

            'Flexiona-se o substantivo no plural, retira - se o s final e acrescenta-se o 
            'sufixo diminutivo.
            'pãe(s) + zinhos        pãezinhos           mão(s) + zinhas     mãozinhas
            'animai(s) + zinhos     animaizinhos        papéi(s) + zinhos   papeizinhos
            'botõe(s) + zinhos      botõezinhos         nuven(s) + zinhas   nuvenzinhas
            'chapéu(s) + zinhos     chapeuzinhos        funi(s) + zinhos    funizinhos
            'farói(s) + zinhos      faroizinhos         túnei(s) + zinhos   tuneizinhos
            'tren(s) + zinhos       trenzinhos          pai(s) + zinhos     paizinhos
            'colhere(s) + zinhas    colherezinhas       pé(s) + zinhos      pezinhos
            'flore(s) + zinhas      florezinhas         pé(s) + zitos       pezitos


            'Obs.: são anômalos os plurais pastorinhos(as), papelinhos, florzinhas, florinhas, colherzinhas e mulherzinhas, correntes na língua popular, e usados até por escritores 
            'de renome.
            If nombre Like "*zinho" Then
                nombre = PluralPor(Mid(nombre, 1, Len(nombre) - Len("zinho")))
                nombre = Mid(nombre, 1, Len(nombre) - 1) & "zinhos"

            ElseIf nombre Like "*zinha" Then

                nombre = PluralPor(Mid(nombre, 1, Len(nombre) - Len("zinha")))
                nombre = Mid(nombre, 1, Len(nombre) - 1) & "zinhas"

            ElseIf nombre Like "*zito" Then

                nombre = PluralPor(Mid(nombre, 1, Len(nombre) - Len("zito")))
                nombre = Mid(nombre, 1, Len(nombre) - 1) & "zitos"

            ElseIf nombre Like "*zita" Then
                nombre = PluralPor(Mid(nombre, 1, Len(nombre) - Len("zita")))
                nombre = Mid(nombre, 1, Len(nombre) - 1) & "zitas"

            ElseIf nombre Like "*[aeiouüúóíéáêâôîûãõ]" Or nombre Like "*[aiuêôéó][aiuêôéó]" Or nombre Like "*n" Then  'Os substantivos terminados em vogal, ditongo oral e n fazem o plural pelo acréscimo de s.
                nombre = nombre & "s"
                'Por exemplo
                'pai -pais
                'ímã -ímãs 
                'hífen -hifens(sem acento, no plural).
                'Exceção: cânon -cânones.
                If nombre = "cânons" Then nombre = "cânones"


            ElseIf nombre Like "*m" Then 'Os substantivos terminados em m fazem o plural em ns.
                nombre = Mid(nombre, 1, Len(nombre) - 1) & "ns"
                'Por exemplo
                'homem -homens.

            ElseIf nombre Like "*[rz]" Then 'Os substantivos terminados em r e z fazem o plural pelo acréscimo de es.
                nombre &= "es"
                'por exemplo
                'revólver -revólveres
                'raiz -raízes
                'Atenção:    O Plural de caráter é caracteres.
                If nombre = "caráteres" Then nombre = "caracteres"

            ElseIf nombre Like "*[aeou]l" Then 'Os substantivos terminados em al, el, ol, ul flexionam-se no plural, trocando o l por is.

                nombre = Mid(nombre, 1, Len(nombre) - 1) & "is"
                'Por exemplo
                'quintal -quintais
                'caracol -caracóis
                'hotel -hotéis
                'Exceções:   mal e males, cônsul e cônsules.
                If nombre = "mais" Then nombre = "males"
                If nombre = "cônsuis" Then nombre = "cônsules"

            ElseIf nombre Like "*il" Then  'Os substantivos terminados em il fazem o plural de duas maneiras:
                'Quando oxítonos, em is.
                'Por exemplo
                'canil -canis
                nombre = Mid(nombre, 1, Len(nombre) - 1) & "s"

                'Quando paroxítonos, em eis. míssil - mísseis.
                '[programar]
                'Obs.:  a palavra réptil pode formar seu plural de duas maneiras: répteis ou reptis (pouco usada).
                If nombre = "reptis" Then nombre = "répteis"

            ElseIf nombre Like "*s" Then 'Os substantivos terminados em s fazem o plural de duas maneiras:  
                '- Quando monossilábicos ou oxítonos, mediante o acréscimo de es.
                'Por exemplo
                'ás -ases
                'retrós -retroses
                nombre &= "es"

                '- Quando paroxítonos ou proparoxítonos, ficam invariáveis.
                'Por exemplo
                'o lápis - os lápis
                'o ônibus - os ônibus.

            ElseIf nombre Like "*ão" Then   'Os substantivos terminados em ão fazem o plural de três maneiras.
                'Não há uma regra específica a ser seguida para se fazer este plural, pois pode variar entre os três e dependerá unicamente da origem da palavra, ou seja, de sua etimologia.

                '- substituindo o -ão por -ões
                'Por exemplo
                'ação -ações


                '- substituindo o -ão por -ães
                'Por exemplo
                'cão -cães

                'Poucos vocábulos tem seu plural em –ães
                Select Case nombre
                    Case "alemão"
                        nombre = "alemães"
                    Case "cão"
                        nombre = "cães"
                    Case "capitão"
                        nombre = "capitães"
                    Case "catalão"
                        nombre = "catalães"
                    Case "charlatão"
                        nombre = "charlatães"
                    Case "escrivão"
                        nombre = "escrivães"
                    Case "guardião"
                        nombre = "guardiães"
                    Case "pão"
                        nombre = "pães"
                    Case "sacristão"
                        nombre = "sacristães"
                    Case "tabelião"
                        nombre = "tabeliães"
                    Case Else
                        nombre = Mid(nombre, 1, Len(nombre) - 2) & "ões"
                End Select

                '- substituindo o -ão por -ãos
                'Por exemplo
                'grão -grãos


            ElseIf nombre Like "*x" Then    'Os substantivos terminados em x ficam invariáveis.
                'Por exemplo
                'o látex - os látex.
                'Excepções: fax - faxes, fénix - as fénixes ou as fénix
                If nombre = "fax" Then nombre = "faxes"
                If nombre = "fénix" Then nombre = "fénixes"


            End If

        Else   'Para pluralizar os substantivos compostos cujos elementos são ligados por hífen, observe as orientações a seguir:
            'a) Quando as duas palavras forem substantivos, pode-se optar em colocar apenas o primeiro elemento ou ambos no plural
            'palavra-chave = palavras-chave ou palavras-chaves
            'couve-flor = couves-flor ou couves-flores
            'bomba-relógio = bombas-relógio ou bombas-relógios
            'peixe-espada = peixes-espada ou peixes-espadas

            'b) Flexionam-se os dois elementos, quando formados de 
            'substantivo +adjetivo = amor-perfeito e amores-perfeitos 
            'adjetivo+substantivo = gentil-homem e gentis-homens
            'numeral+substantivo = quinta-feira e quintas-feiras

            'c) Flexiona-se somente o segundo elemento, quando formados de 
            'Verbo+substantivo = guarda - roupa e guarda-roupas 
            'palavra invariável + palavra variável = alto-falante e alto-falantes 
            'palavras repetidas ou imitativas = reco-reco e reco-recos

            'd) Flexiona-se somente o primeiro elemento, quando formados de 
            'substantivo +preposição clara + substantivo = água-de-colônia e águas-de-colônia
            'substantivo +preposição oculta + substantivo = cavalo-vapor e cavalos-vapor

            'e) Permanecem invariáveis, quando formados de 
            'Verbo +advérbio = o bota-fora e os bota-fora 
            'Verbo +substantivo no plural = o saca-rolhas e os saca-rolhas





            Dim j() = Split(nombre, "-")

            If j(0) = j(1) Then
                nombre = j(0) & "-" & PluralPor(j(1)) 'sólo el segundo elemento

            ElseIf UBound(j) > 1 Then 'substantivo + preposição clara + substantivo = água-de-colônia e águas-de-colônia

                'f) Casos Especiais
                Select Case nombre
                    Case "louva-a-deus" 'o louva-a-deus e os louva-a-deus
                        nombre = "louva-a-deus"
                    Case "bem-te-vi" 'o bem-te-vi e os bem-te-vis
                        nombre = "bem-te-vis"
                    Case "bem-Me-quer" 'o bem-Me-quer e os bem-me-queres
                        nombre = "bem-me-queres"
                    Case "joão-ninguém" 'o joão-ninguém e os joões-ninguém.
                        nombre = "joões-ninguém"
                    Case Else
                        nombre = PluralPor(j(0)) & "-" & j(1) & "-" & j(2) 'solo el primer elemento
                End Select


            Else
                nombre = PluralPor(j(0)) & "-" & PluralPor(j(1)) 'ambos elementos
            End If


        End If
        Return nombre

    End Function

    Function VogalOral(s As String) As Boolean
        If s Like "[aiuêôéó]" Then
            Return True
        Else
            Return False
        End If
    End Function

End Module
