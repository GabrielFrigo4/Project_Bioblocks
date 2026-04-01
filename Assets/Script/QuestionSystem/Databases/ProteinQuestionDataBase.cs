using UnityEngine;
using System.Collections.Generic;
using QuestionSystem;

public class ProteinQuestionDatabase : MonoBehaviour, IQuestionDatabase
{

    [Header("Development Settings")]
    [SerializeField] private bool databaseInDevelopment = false;
    
    private List<Question> questions = new List<Question>
    {
         new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "A = Ponte de Hidrogênio, B = Interação Eletrostática, C = Interação Hidrofóbica, D = Ponte Dissulfeto",
                "A = Ponte de Dissulfeto, B = Interação Eletrostática, C = Interação Hidrofóbica, D = Ponte de Hidrogênio",
                "A = Interação Hidrofóbica, B = Ponte de Hidrogênio, C = Ponte Dissulfeto, D = Interação Eletrostática",
                "A = Interação Eletrostática, B = Ponte de Hidrogênio, C = Interação Hidrofóbica, D = Ponte Dissulfeto",
            },
            correctIndex = 2,
            questionNumber = 1,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "AnswerImages/ProteinDB/proteinQuestion_1",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "Que tipo de ligação une os aminoácidos na estrutura primária de uma proteína?",
            answers = new string[] {
                "Ligações de hidrogênio",
                "Ligações iônicas",
                "Ligações peptídicas",
                "Pontes dissulfeto"
            },
            correctIndex = 2,
            questionNumber = 2,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "A formação de uma ligação peptídica envolve:",
            answers = new string[] {
                "A união de dois aminoácidos com a perda de uma molécula de água.",
                "A união de dois aminoácidos com a adição de uma molécula de água.",
                "A união de três aminoácidos com a perda de uma molécula de água.",
                "A união de três aminoácidos com a adição de uma molécula de água."
            },
            correctIndex = 0,
            questionNumber = 3,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "Identifique abaixo o aminoácido que pode formar pontes dissulfeto em proteínas",
            answers = new string[] {
                "AnswerImages/AminoacidsDB/cisteina",
                "AnswerImages/AminoacidsDB/treonina",
                "AnswerImages/AminoacidsDB/alanina",
                "AnswerImages/AminoacidsDB/fenilalanina"
            },
            correctIndex = 0,
            questionNumber = 4,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "Que tipo de estrutura protéica é formada pela união de duas ou mais cadeias polipeptídicas?",
            answers = new string[] {
                "Primária",
                "Secundária",
                "Terciária",
                "Quaternária"
            },
            correctIndex = 3,
            questionNumber = 5,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Uma estrutura formada basicamente por alfa-hélices",
                "Uma estrutura formada basicamente por fitas-betas",
                "A imagem representa apenas a estrutura primária de uma proteína",
                "Não há estrutura definida",
            },
            correctIndex = 0,
            questionNumber = 6,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "AnswerImages/ProteinDB/proteinQuestion_6",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "Quais são os principais elementos de estrutura secundária?",
            answers = new string[] {
                "Alfa-hélices e fitas-betas",
                "Alfa-hélices e pontes dissulfeto",
                "Fitas-betas e ligações peptídicas",
                "Voltas e ligações iônicas"
            },
            correctIndex = 0,
            questionNumber = 7,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Interações hidrofóbicas",
                "Pontes de hidrogênio",
                "Ligações iônicas",
                "Pontes dissulfeto"
            },
            correctIndex = 1,
            questionNumber = 8,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "AnswerImages/ProteinDB/proteinQuestion_8",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/AminoacidsDB/acido_aspartico",
                "AnswerImages/AminoacidsDB/glutamina",
                "AnswerImages/AminoacidsDB/arginina",
                "AnswerImages/AminoacidsDB/prolina"
            },
            correctIndex = 3,
            questionNumber = 9,
            isImageAnswer = true,
            isImageQuestion = true,
            questionImagePath = "AnswerImages/ProteinDB/proteinQuestion_9",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "A estrutura terciária de uma proteína se refere a:",
            answers = new string[] {
                "Seqüência de aminoácidos.",
                "Arranjo espacial de alfa-hélices e folhas beta.",
                "Interações entre diferentes cadeias polipeptídicas.",
                "Estrutura tridimensional de uma sequência de aminoácidos."
            },
            correctIndex = 3,
            questionNumber = 10,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "Quais forças mantêm a estrutura terciária de uma proteína?",
            answers = new string[] {
                "Apenas ligações peptídicas.",
                "Pontes de hidrogênio, interações hidrofóbicas, interações iônicas e pontes dissulfeto.",
                "Apenas pontes dissulfeto.",
                "Apenas ligações de hidrogênio."
            },
            correctIndex = 1,
            questionNumber = 11,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "Identifique abaixo a melhor representação de estrutura secundaria",
            answers = new string[] {
                "AnswerImages/ProteinDB/estrutura_primaria",
                "AnswerImages/ProteinDB/estrutura_secundaria",
                "AnswerImages/ProteinDB/estrutura_terciaria",
                "AnswerImages/ProteinDB/estrutura_quaternaria"
            },
            correctIndex = 1,
            questionNumber = 12,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/AminoacidsDB/acido_aspartico",
                "AnswerImages/AminoacidsDB/treonina",
                "AnswerImages/AminoacidsDB/arginina",
                "AnswerImages/AminoacidsDB/isoleucina"
            },
            correctIndex = 3,
            questionNumber = 13,
            isImageAnswer = true,
            isImageQuestion = true,
            questionImagePath = "AnswerImages/ProteinDB/proteinQuestion_13",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/AminoacidsDB/triptofano",
                "AnswerImages/AminoacidsDB/isoleucina",
                "AnswerImages/AminoacidsDB/arginina",
                "AnswerImages/AminoacidsDB/fenilalanina"
            },
            correctIndex = 2,
            questionNumber = 14,
            isImageAnswer = true,
            isImageQuestion = true,
            questionImagePath = "AnswerImages/ProteinDB/proteinQuestion_14",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "Identifique abaixo a melhor representação de estrutura quaternaria",
            answers = new string[] {
                "AnswerImages/ProteinDB/estrutura_primaria",
                "AnswerImages/ProteinDB/estrutura_secundaria",
                "AnswerImages/ProteinDB/estrutura_terciaria",
                "AnswerImages/ProteinDB/estrutura_quaternaria"
            },
            correctIndex = 3,
            questionNumber = 15,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "Identifique abaixo a melhor representação de estrutura primaria",
            answers = new string[] {
                "AnswerImages/ProteinDB/estrutura_primaria",
                "AnswerImages/ProteinDB/estrutura_secundaria",
                "AnswerImages/ProteinDB/estrutura_terciaria",
                "AnswerImages/ProteinDB/estrutura_quaternaria"
            },
            correctIndex = 0,
            questionNumber = 16,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "Identifique abaixo a melhor representação de estrutura terciária",
            answers = new string[] {
                "AnswerImages/ProteinDB/estrutura_primaria",
                "AnswerImages/ProteinDB/estrutura_secundaria",
                "AnswerImages/ProteinDB/estrutura_terciaria",
                "AnswerImages/ProteinDB/estrutura_quaternaria"
            },
            correctIndex = 2,
            questionNumber = 17,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
         new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "alfa-hélices",
                "fitas-beta",
                "folhas-beta",
                "beta-hélices",
            },
            correctIndex = 0,
            questionNumber = 18,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "AnswerImages/ProteinDB/proteinQuestion_19",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "alfa-hélices",
                "fitas-beta",
                "fitas-alfa",
                "beta-hélices",
            },
            correctIndex = 1,
            questionNumber = 19,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "AnswerImages/ProteinDB/proteinQuestion_20",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Cromatografia de focalização isoelétrica",
                "Cromatografia de gel filtração",
                "Cromatrografia de troca iônica",
                "Cromatrografia de afinidade",
            },
            correctIndex = 2,
            questionNumber = 20,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "AnswerImages/ProteinDB/methodsQuestions20",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Cromatografia de focalização isoelétrica",
                "Cromatografia de gel filtração",
                "Cromatrografia de troca iônica",
                "Cromatrografia de afinidade",
            },
            correctIndex = 1,
            questionNumber = 21,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "AnswerImages/ProteinDB/methodsQuestions21",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "Imagine uma mistura de duas proteínas (15 kDa e 45 kDa). Usando uma coluna de exclusão que interage com proteínas de até 20 kDa, qual proteína sairá primeiro da coluna?",
            answers = new string[] {
                "Ambas saírão juntas",
                "12 kDa sairá primeiro",
                "45 kDa sairá primeiro",
                "A coluna não conseguirá separar as proteínas",
            },
            correctIndex = 2,
            questionNumber = 22,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "Imagine uma mistura de duas proteínas com pI = 6 e pI = 12. Usando uma coluna trocadora de cátions, qual proteína sairá primeiro da coluna com uma fase móvel com pH = 9?",
            answers = new string[] {
                "Ambas saírão juntas",
                "Proteína com pI = 6 sairá primeiro",
                "Proteína com pI = 9 sairá primeiro",
                "A coluna não conseguirá separar as proteínas",
            },
            correctIndex = 1,
            questionNumber = 23,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "A técnica de eletroforese SDS-Page separa proteínas com base em:",
            answers = new string[] {
                "Seu tamanho.",
                "Sua carga líquida.",
                "Sua sequência de aminoácidos.",
                "Sua estrutura terciária."
            },
            correctIndex = 0,
            questionNumber = 24,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "Em eletroforese com focalização isoelétrica, um aminoácido com carga líquida positiva migrará para o pólo:",
            answers = new string[] {
                "Positivo",
                "Negativo",
                "Não migrará",
                "Depende do pH"
            },
            correctIndex = 1,
            questionNumber = 25,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "Em eletroforese com focalização isoelétrica, um aminoácido com carga líquida negativa migrará para o pólo:",
            answers = new string[] {
                "Negativo",
                "Positivo",
                "Não migrará",
                "Depende do pH"
            },
            correctIndex = 1,
            questionNumber = 26,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "Em eletroforese com focalização isoelétrica, um aminoácido no seu ponto isoelétrico:",
            answers = new string[] {
                "Migrará para o pólo positivo.",
                "Migrará para o pólo negativo.",
                "Não migrará.",
                "Migrará para ambos os pólos."
            },
            correctIndex = 2,
            questionNumber = 27,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "A curva na cor vermelha refere-se a uma proteína",
                "A curva na cor azul refere-se a uma proteína",
                "A curva na cor vermelha refere-se ao aminoácido alanina",
                "A curva na cor azul refere-se ao aminoácido leucina"
            },
            correctIndex = 1,
            questionNumber = 28,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "AnswerImages/ProteinDB/methodsQuestions28",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Transportar oxigênio",
                "Manter estrutura das hemácias",
                "Contração dos pulmões",
                "Armazenar oxigênio"
            },
            correctIndex = 3,
            questionNumber = 29,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "AnswerImages/ProteinDB/function_and_structure29",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Transportar oxigênio",
                "Manter estrutura das hemácias",
                "Contração dos pulmões",
                "Armazenar oxigênio"
            },
            correctIndex = 0,
            questionNumber = 30,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "AnswerImages/ProteinDB/function_and_structure30",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Hemoglobina",
                "Mioglobina",
                "Não há diferença",
                "Ambas as proteínas tem baixa afinidade por oxigênio"
            },
            correctIndex = 1,
            questionNumber = 31,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "AnswerImages/ProteinDB/function_and_structure31",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "A mioglobina tem uma estrutura quaternária que facilita a ligação com O<sub><size=150%>2</size></sub>",
                "A hemoblogina pode assumir duas conformações, R e T, que têm diferentes afinidades por O<sub><size=150%>2</size></sub> ",
                "As proteínas não apresentam diferenças de afinidade por O<sub><size=150%>2</size></sub>",
                "Porque a hoglobina liga-se a quatro grupos heme"
            },
            correctIndex = 1,
            questionNumber = 32,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "AnswerImages/ProteinDB/function_and_structure32",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "As proteínas são polímeros formados por:",
            answers = new string[] {
                "Monossacarídeos",
                "Nucleotídeos",
                "Aminoácidos",
                "Lipídios"
            },
            correctIndex = 2,
            questionNumber = 34,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "Qual é a ligação que une os aminoácidos em uma proteína?",
            answers = new string[] {
                "Ligação iônica",
                "Ligação de hidrogênio",
                "Ligação dissulfeto",
                "Ligação peptídica"
            },
            correctIndex = 3,
            questionNumber = 35,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "A sequência linear de aminoácidos em uma proteína define sua:",
            answers = new string[] {
                "Estrutura primária",
                "Estrutura secundária",
                "Estrutura terciária",
                "Estrutura quaternária"
            },
            correctIndex = 0,
            questionNumber = 36,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "As estruturas secundárias mais comuns das proteínas são:",
            answers = new string[] {
                "Hélice alfa e folha beta",
                "Dobra gama e alfa-barril",
                "Hélice dupla e tríplice folha",
                "Folha zeta e hélice delta"
            },
            correctIndex = 0,
            questionNumber = 37,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "A estrutura terciária de uma proteína está relacionada à:",
            answers = new string[] {
                "Sequência de nucleotídeos",
                "Dobra tridimensional da cadeia polipeptídica",
                "Associação de várias subunidades",
                "Formação de pontes de hidrogênio apenas"
            },
            correctIndex = 1,
            questionNumber = 38,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "Quando várias cadeias polipeptídicas se associam para formar uma proteína funcional, isso corresponde à:",
            answers = new string[] {
                "Estrutura primária",
                "Estrutura secundária",
                "Estrutura terciária",
                "Estrutura quaternária"
            },
            correctIndex = 3,
            questionNumber = 39,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "Qual das funções abaixo não é típica das proteínas?",
            answers = new string[] {
                "Catálise de reações (enzimas)",
                "Transporte de substâncias",
                "Defesa imunológica",
                "Armazenamento de informação genética"
            },
            correctIndex = 3,
            questionNumber = 40,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "A hemoglobina é um exemplo de proteína que exerce função de:",
            answers = new string[] {
                "Reserva energética",
                "Transporte de oxigênio",
                "Defesa contra patógenos",
                "Catalisar reações"
            },
            correctIndex = 1,
            questionNumber = 41,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "O processo em que uma proteína perde sua forma funcional devido a alterações de temperatura ou pH chama-se:",
            answers = new string[] {
                "Desnaturação",
                "Oxidação",
                "Polimerização",
                "Transcrição"
            },
            correctIndex = 0,
            questionNumber = 42,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "As enzimas, que são catalisadores biológicos, são classificadas quimicamente como:",
            answers = new string[] {
                "Proteínas",
                "Lipídios",
                "Polissacarídeos",
                "Ácidos nucleicos"
            },
            correctIndex = 0,
            questionNumber = 43,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "As proteínas são polímeros formados por unidades chamadas:",
            answers = new string[] {
                "Monossacarídeos",
                "Nucleotídeos",
                "Aminoácidos",
                "Aminoácidos"
            },
            correctIndex = 2,
            questionNumber = 44,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "A ligação que une os aminoácidos em uma proteína é chamada:",
            answers = new string[] {
                "Glicosídica",
                "Peptídica",
                "Fosfodiéster",
                "Iônica"
            },
            correctIndex = 1,
            questionNumber = 45,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "Qual estrutura da proteína corresponde à sequência linear de aminoácidos?",
            answers = new string[] {
                "Estrutura primária",
                "Estrutura secundária",
                "Estrutura terciária",
                "Estrutura quaternária"
            },
            correctIndex = 0,
            questionNumber = 46,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "As hélices alfa e folhas beta são exemplos de qual nível de organização proteica?",
            answers = new string[] {
                "Primário",
                "Secundário",
                "Terciário",
                "Quaternário"
            },
            correctIndex = 1,
            questionNumber = 47,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "A hemoglobina é exemplo de proteína com estrutura:",
            answers = new string[] {
                "Primário",
                "Secundário",
                "Terciário",
                "Quaternário"
            },
            correctIndex = 3,
            questionNumber = 48,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "Qual é a função principal das enzimas, que são proteínas especializadas?",
            answers = new string[] {
                "Armazenar energia",
                "Catalisar reações químicas",
                "Transportar gases",
                "Formar membranas"
            },
            correctIndex = 1,
            questionNumber = 49,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "O colágeno, proteína encontrada em tecidos conjuntivos, tem função principalmente:",
            answers = new string[] {
                "Energética",
                "Estrutural",
                "Catalítica",
                "Hormonal"
            },
            correctIndex = 1,
            questionNumber = 50,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "A desnaturação de uma proteína pode ser causada por:",
            answers = new string[] {
                "Aumento de temperatura ou variação extrema de pH",
                "Formação de ligação peptídica",
                "Diminuição da concentração de glicose",
                "Ação de oxigênio atmosférico"
            },
            correctIndex = 0,
            questionNumber = 51,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "A insulina é um exemplo de proteína com função:",
            answers = new string[] {
                "Hormonal",
                "Estrutural",
                "Energética",
                "Enzimática"
            },
            correctIndex = 0,
            questionNumber = 52,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "As proteínas podem atuar no transporte de moléculas. Um exemplo é:",
            answers = new string[] {
                "DNA",
                "Hemoglobina",
                "Glicogênio",
                "Amido"
            },
            correctIndex = 1,
            questionNumber = 53,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "A ligação que une os aminoácidos na formação das proteínas é chamada de:",
            answers = new string[] {
                "Ligação de hidrogênio",
                "Ligação glicosídica",
                "Ligação peptídica",
                "Ligação fosfodiéster"
            },
            correctIndex = 2,
            questionNumber = 54,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "A estrutura tridimensional que representa o dobramento total da proteína é a:",
            answers = new string[] {
                "Estrutura primária",
                "Estrutura secundária",
                "Estrutura terciária",
                "Estrutura quaternária"
            },
            correctIndex = 2,
            questionNumber = 55,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "A sequência linear de aminoácidos em uma proteína corresponde à:",
            answers = new string[] {
                "Estrutura primária",
                "Estrutura secundária",
                "Estrutura terciária",
                "Estrutura quaternária"
            },
            correctIndex = 0,
            questionNumber = 56,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "A hemoglobina é um exemplo de proteína com:",
            answers = new string[] {
                "Estrutura primária",
                "Estrutura secundária",
                "Estrutura terciária",
                "Estrutura quaternária"
            },
            correctIndex = 3,
            questionNumber = 57,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "As proteínas podem atuar como:",
            answers = new string[] {
                "Catalisadores biológicos, hormônios e elementos estruturais",
                "Fonte primária de energia imediata, como a glicose",
                "Material genético das células",
                "Lipídios armazenados em vacúolos"
            },
            correctIndex = 0,
            questionNumber = 58,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "A desnaturação de uma proteína ocorre quando:",
            answers = new string[] {
                "Ela perde sua sequência primária",
                "Suas interações químicas são rompidas, alterando sua forma",
                "Há aumento da quantidade de aminoácidos",
                "Ela se torna insolúvel em lipídeos"
            },
            correctIndex = 1,
            questionNumber = 59,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "A desnaturação de uma proteína ocorre quando:",
            answers = new string[] {
                "Ela perde sua sequência primária",
                "Suas interações químicas são rompidas, alterando sua forma",
                "Há aumento da quantidade de aminoácidos",
                "Ela se torna insolúvel em lipídeos"
            },
            correctIndex = 1,
            questionNumber = 60,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "Qual dessas proteínas atua no transporte de oxigênio no sangue?",
            answers = new string[] {
                "Insulina",
                "Hemoglobina",
                "Colágeno",
                "Tripsina"
            },
            correctIndex = 1,
            questionNumber = 61,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "A estrutura primária de uma proteína refere-se a:",
            answers = new string[] {
                "O enovelamento tridimensional da proteína",
                "A sequência linear de aminoácidos",
                "A associação de várias cadeias polipeptídicas",
                "A formação de hélices alfa e folhas beta"
            },
            correctIndex = 1,
            questionNumber = 62,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "A insulina é um exemplo de proteína com função:",
            answers = new string[] {
                "Estrutural",
                "Hormonal",
                "Catalítica",
                "Energética"
            },
            correctIndex = 1,
            questionNumber = 63,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "A insulina é um exemplo de proteína com função:",
            answers = new string[] {
                "Estrutural",
                "Hormonal",
                "Catalítica",
                "Energética"
            },
            correctIndex = 1,
            questionNumber = 64,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "As proteínas podem atuar como:",
            answers = new string[] {
                "Catalisadores biológicos (enzimas)",
                "Moléculas de armazenamento de energia imediata",
                "Componentes principais da membrana nuclear",
                "Moléculas de informação genética"
            },
            correctIndex = 0,
            questionNumber = 65,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "Qual desses fatores pode desnaturar uma proteína?",
            answers = new string[] {
                "Temperatura elevada",
                "pH extremo",
                "Presença de solventes orgânicos",
                "Todas as anteriores"
            },
            correctIndex = 3,
            questionNumber = 66,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "Qual desses fatores pode desnaturar uma proteína?",
            answers = new string[] {
                "Temperatura elevada",
                "pH extremo",
                "Presença de solventes orgânicos",
                "Todas as anteriores"
            },
            correctIndex = 3,
            questionNumber = 67,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "A albumina presente no ovo é uma proteína com função principalmente:",
            answers = new string[] {
                "Hormonal",
                "Energética e de reserva",
                "Estrutural",
                "Transportadora de oxigênio"
            },
            correctIndex = 1,
            questionNumber = 68,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "As proteínas chamadas fibrosas, como a queratina e o colágeno, têm função principal:",
            answers = new string[] {
                "Hormonal",
                "Catalítica",
                "Estrutural",
                "De defesa imunológica"
            },
            correctIndex = 2,
            questionNumber = 69,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "Os anticorpos (imunoglobulinas) são proteínas com função:",
            answers = new string[] {
                "Energética",
                "Estrutural",
                "De defesa",
                "Hormonal"
            },
            correctIndex = 2,
            questionNumber = 70,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "ProteinQuestionDatabase",
            questionText = "As proteínas que atuam como hormônios, como a insulina, têm função:",
            answers = new string[] {
                "Estrutural",
                "Reguladora",
                "Energética",
                "De transporte"
            },
            correctIndex = 1,
            questionNumber = 71,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        }
    };

    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.proteins;
    public string GetDatabankName()  => "ProteinQuestionDatabase";
    public string GetDisplayName()   => "Proteínas";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;
}
