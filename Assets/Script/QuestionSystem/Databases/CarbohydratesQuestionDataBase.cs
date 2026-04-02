using UnityEngine;
using System.Collections.Generic;
using QuestionSystem;

public class CarbohydratesQuestionDatabase : MonoBehaviour, IQuestionDatabase
{

    [Header("Development Settings")]
    [SerializeField] private bool databaseInDevelopment = false;
    
    private List<Question> questions = new List<Question>
    {
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Qual a fórmula geral dos monossacarídeos?",
            answers = new string[] {
                "(CH<sub>2</sub> O)<sub>n</sub>",
                "C<sub>n</sub> H<sub>2n</sub> O<sub>n</sub>",
                "C<sub>n</sub> H<sub>2n-2</sub> O<sub>n</sub>",
                "C<sub>n</sub> H<sub>2n+2</sub> O<sub>n</sub>"
            },
            correctIndex = 0,
            questionNumber = 1,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "O que diferencia uma aldose de uma cetose?",
            answers = new string[] {"Grupo funcional aldeído vs. cetona", "Número de átomos de carbono", "Presença de oxigênio", "Solubilidade em água"},
            correctIndex = 0,
            questionNumber = 2,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Qual das estruturas representa uma cetose?",
            answers = new string[] {
                "AnswerImages/CarbohydrateDB/D-aldotriose",
                "AnswerImages/CarbohydrateDB/D-aldotetrose",
                "AnswerImages/CarbohydrateDB/D-cetotetrose",
                "AnswerImages/CarbohydrateDB/D-aldopentose"
            },
            correctIndex = 2,
            questionNumber = 3,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Qual estrutura representa uma aldotetrose",
            answers = new string[] {
                "AnswerImages/CarbohydrateDB/D-aldotriose",
                "AnswerImages/CarbohydrateDB/D-aldotetrose",
                "AnswerImages/CarbohydrateDB/D-cetotetrose",
                "AnswerImages/CarbohydrateDB/D-aldopentose"
            },
            correctIndex = 1,
            questionNumber = 4,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/CarbohydrateDB/L-glicose",
                "AnswerImages/CarbohydrateDB/D-glicose",
                "AnswerImages/CarbohydrateDB/D-galactose",
                "AnswerImages/CarbohydrateDB/D-frutose"
            },
            correctIndex = 0,
            questionNumber = 5,
            isImageAnswer = true,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/CarbohydrateDB/carbohydrateDB_ImageQuestionContainer5",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "O que são epímeros?",
            answers = new string[] {
                "Diastereoisômeros que diferem em um único centro quiral",
                "Enanciômeros que diferem em todos os centros quirais",
                "Isômeros que diferem no número de átomos de carbono",
                "Isômeros que diferem no tipo de ligação"},
            correctIndex = 0,
            questionNumber = 6,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/CarbohydrateDB/L-glicose",
                "AnswerImages/CarbohydrateDB/D-glicose",
                "AnswerImages/CarbohydrateDB/L-frutose",
                "AnswerImages/CarbohydrateDB/D-tagatose"
            },
            correctIndex = 3,
            questionNumber = 7,
            isImageAnswer = true,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/CarbohydrateDB/carbohydrateDB_ImageQuestionContainer7",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "O que são anômeros?",
            answers = new string[] {
                "Isômeros cíclicos que diferem na configuração do carbono anomérico",
                "Isômeros de cadeia aberta",
                "Isômeros que diferem na posição do grupo hidroxila",
                "Isômeros que diferem no tipo de ligação"
            },
            correctIndex = 0,
            questionNumber = 8,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/CarbohydrateDB/L-glicose",
                "AnswerImages/CarbohydrateDB/alfa-galactopiranose",
                "AnswerImages/CarbohydrateDB/beta-galactosamina",
                "AnswerImages/CarbohydrateDB/alfa-glicopiranose"
            },
            correctIndex = 3,
            questionNumber = 9,
            isImageAnswer = true,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/CarbohydrateDB/carbohydrateDB_ImageQuestionContainer9",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Qual o principal dissacarídeo encontrado na cana-de-açúcar?",
            answers = new string[] {"Maltose", "Lactose", "Sacarose", "Celobiose"},
            correctIndex = 2,
            questionNumber = 10,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Qual dissacarídeo é o açúcar do leite?",
            answers = new string[] {"Maltose", "Lactose", "Sacarose", "Celobiose"},
            correctIndex = 1,
            questionNumber = 11,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Qual o tipo de ligação que une os monossacarídeos em um dissacarídeo?",
            answers = new string[] {"Ligação peptídica", "Ligação glicosídica", "Ligação éster", "Ligação fosfodiéster"},
            correctIndex = 1,
            questionNumber = 12,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "monossacarídeo",
                "dissacarídeo",
                "oligossacarídeo",
                "polissacarídeo"
            },
            correctIndex = 1,
            questionNumber = 13,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/CarbohydrateDB/carbohydrateDB_ImageQuestionContainer13",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "1-beta-4",
                "1-alfa-4",
                "1-beta-2",
                "1-alfa-2"
            },
            correctIndex = 0,
            questionNumber = 14,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/CarbohydrateDB/carbohydrateDB_ImageQuestionContainer14",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Qual o principal polissacarídeo de reserva energética em plantas?",
            answers = new string[] {"Celulose", "Quitina", "Amido", "Glicogênio"},
            correctIndex = 2,
            questionNumber = 15,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Qual o principal polissacarídeo de reserva energética em animais?",
            answers = new string[] {"Celulose", "Quitina", "Amido", "Glicogênio"},
            correctIndex = 3,
            questionNumber = 16,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Qual a principal diferença estrutural entre amilose e amilopectina?",
            answers = new string[] {"Ramificação", "Tipo de ligação glicosídica", "Tipo de monossacarídeo", "Peso molecular"},
            correctIndex = 0,
            questionNumber = 17,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Qual o tipo de ligação glicosídica predominante na celulose?",
            answers = new string[] {"alfa(1 -> 4)", "alfa(1 -> 6)", "beta(1 -> 4)", "beta(1 -> 6)"},
            correctIndex = 2,
            questionNumber = 18,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Qual o tipo de ligação glicosídica predominante no amido?",
            answers = new string[] {"alfa(1 -> 4)", "alfa(1 -> 6)", "beta(1 -> 4)", "beta(1 -> 6)"},
            correctIndex = 0,
            questionNumber = 19,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Qual enzima hidrolisa o amido?",
            answers = new string[] {"Celulase", "Quitina", "Amilase", "Lactase"},
            correctIndex = 2,
            questionNumber = 20,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Qual enzima hidrolisa a celulose?",
            answers = new string[] {"Celulase", "Quitina", "Amilase", "Lactase"},
            correctIndex = 0,
            questionNumber = 21,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "O que são glicoproteínas?",
            answers = new string[] {
                "Proteínas ligadas a carboidratos",
                "Proteínas ligadas a lipídeos",
                "Proteínas ligadas a ácidos nucleicos",
                "Proteínas ligadas a outras proteínas"
            },
            correctIndex = 0,
            questionNumber = 22,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "O que são glicosaminoglicanos?",
            answers = new string[] {
                "Homopolissacarídeos que presentes no citoplasma celular.",
                "Heteropolissacarídeos que compõe a matriz extracelular.",
                "São glicoses ligadas a aminas presentes no sangue",
                "São dissacarídeos ligados à proteínas"
            },
            correctIndex = 1,
            questionNumber = 23,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "O que são proteoglicanos?",
            answers = new string[] {
                "Glicosaminoglicanos ligados a proteínas",
                "Glicoproteínas ligadas a lipídeos",
                "Glicolipídeos ligados a proteínas",
                "Proteínas ligadas a ácidos nucleicos"
            },
            correctIndex = 0,
            questionNumber = 24,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Onde são encontradas os glicosaminoglicanos?",
            answers = new string[] {"Matriz extracelular", "Membrana celular", "Citoplasma", "Todas as alternativas acima"},
            correctIndex = 0,
            questionNumber = 25,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Identifique abaixo um componente estrutural dos glicosaminoglicanos.",
            answers = new string[] {
                "AnswerImages/CarbohydrateDB/alfa-glicopiranose",
                "AnswerImages/CarbohydrateDB/beta-glicopiranose",
                "AnswerImages/CarbohydrateDB/beta-galactosamina",
                "AnswerImages/CarbohydrateDB/D-aldopentose"
            },
            correctIndex = 2,
            questionNumber = 26,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Indique abaixo a estrutura do ácido hialurônico, que é um glicosaminoglicano cujo monômero é formado pelo ácido glicurônico e pelo ácido N-acetil-glicosamina.",
            answers = new string[] {
                "AnswerImages/CarbohydrateDB/dermatan",
                "AnswerImages/CarbohydrateDB/condroitin",
                "AnswerImages/CarbohydrateDB/queratan",
                "AnswerImages/CarbohydrateDB/hialuronico"
            },
            correctIndex = 3,
            questionNumber = 27,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Qual a função do ácido hialurônico?",
            answers = new string[] {"Lubrificação", "Suporte estrutural", "Viscosidade", "Todas as alternativas acima"},
            correctIndex = 3,
            questionNumber = 28,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "A heparina é um glicosaminoglicano com uma das maiores densidades de carga negativa dentre todas as biomoléculas dos organismos vivos. Indique abaixo o monômero que forma a heparina.",
            answers = new string[] {
                "AnswerImages/CarbohydrateDB/dermatan",
                "AnswerImages/CarbohydrateDB/condroitin",
                "AnswerImages/CarbohydrateDB/heparina",
                "AnswerImages/CarbohydrateDB/hialuronico"
            },
            correctIndex = 2,
            questionNumber = 29,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Qual é uma das principais função da heparina?",
            answers = new string[] {"Anticoagulante", "Lubrificante", "Suporte estrutural", "Hormônio"},
            correctIndex = 0,
            questionNumber = 30,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "1-beta-4",
                "1-alfa-4",
                "1-beta-3",
                "1-alfa-3"
            },
            correctIndex = 2,
            questionNumber = 31,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/CarbohydrateDB/carbohydrateDB_ImageQuestionContainer31",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Qual a principal função do condroitin?",
            answers = new string[] {"Suporte estrutural em cartilagens", "hormônio", "Neurotransmissor", "Todas as alternativas acima"},
            correctIndex = 0,
            questionNumber = 32,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "1-beta-4",
                "1-alfa-4",
                "1-beta-3",
                "1-alfa-3"
            },
            correctIndex = 0,
            questionNumber = 33,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/CarbohydrateDB/carbohydrateDB_ImageQuestionContainer33",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/CarbohydrateDB/L-glicose",
                "AnswerImages/CarbohydrateDB/D-frutose",
                "AnswerImages/CarbohydrateDB/alfa-galactopiranose",
                "AnswerImages/CarbohydrateDB/D-galactose"
            },
            correctIndex = 1,
            questionNumber = 34,
            isImageAnswer = true,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/CarbohydrateDB/carbohydrateDB_ImageQuestionContainer34",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/CarbohydrateDB/L-glicose",
                "AnswerImages/CarbohydrateDB/D-frutose",
                "AnswerImages/CarbohydrateDB/D-tagatose",
                "AnswerImages/CarbohydrateDB/D-glicose"
            },
            correctIndex = 3,
            questionNumber = 35,
            isImageAnswer = true,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/CarbohydrateDB/carbohydrateDB_ImageQuestionContainer35",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/CarbohydrateDB/beta-galactopiranose",
                "AnswerImages/CarbohydrateDB/alfa-galactopiranose",
                "AnswerImages/CarbohydrateDB/beta-glicopiranose",
                "AnswerImages/CarbohydrateDB/alfa-glicopiranose"
            },
            correctIndex = 2,
            questionNumber = 36,
            isImageAnswer = true,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/CarbohydrateDB/carbohydrateDB_ImageQuestionContainer36",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Identifique abaixo a estrutura da glicose, que também pode ser nomeada como beta-D-glicopiranose",
            answers = new string[] {
                "AnswerImages/CarbohydrateDB/beta-galactopiranose",
                "AnswerImages/CarbohydrateDB/alfa-galactopiranose",
                "AnswerImages/CarbohydrateDB/alfa-glicopiranose",
                "AnswerImages/CarbohydrateDB/beta-glicopiranose"
            },
            correctIndex = 3,
            questionNumber = 37,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/CarbohydrateDB/beta-galactopiranose",
                "AnswerImages/CarbohydrateDB/alfa-galactopiranose",
                "AnswerImages/CarbohydrateDB/alfa-glicopiranose",
                "AnswerImages/CarbohydrateDB/beta-glicopiranose"
            },
            correctIndex = 2,
            questionNumber = 38,
            isImageAnswer = true,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/CarbohydrateDB/carbohydrateDB_ImageQuestionContainer38",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Qual a diferença entre um homopolissacarídeo e um heteropolissacarídeo?",
            answers = new string[] {
                "Um homopolissacarídeo contém apenas um tipo de monossacarídeo, um heteropolissacarídeo contém múltiplos tipos",
                "Um homopolissacarídeo é linear, um heteropolissacarídeo é ramificado",
                "Um homopolissacarídeo é solúvel em água, um heteropolissacarídeo é insolúvel",
                "Um homopolissacarídeo é um polímero, um heteropolissacarídeo é um monômero"
            },
            correctIndex = 0,
            questionNumber = 39,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Dê exemplos de homopolissacarídeos.",
            answers = new string[] {
                "Amido, glicogênio, celulose",
                "Sacarose, lactose, maltose",
                "Glicose, frutose, galactose",
                "Todos os polissacarídeos são homopolissacarídeos"},
            correctIndex = 0,
            questionNumber = 40,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Dê exemplos de heteropolissacarídeos.",
            answers = new string[] {
                "Amido, glicogênio, celulose",
                "Sacarose, lactose, maltose",
                "Peptídeoglicano, glicosaminoglicanos",
                "Glicose, frutose, galactose"
            },
            correctIndex = 2,
            questionNumber = 41,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Qual a principal diferença entre a estrutura da amilose e da celulose?",
            answers = new string[] {
                "A amilose é ramificada, a celulose é linear.",
                "A amilose possui ligações alfa(1 -> 4), a celulose possui ligações beta(1 -> 4).",
                "A amilose é um polímero de frutose, a celulose é um polímero de glicose.",
                "A amilose é insolúvel em água, a celulose é solúvel em água."
            },
            correctIndex = 1,
            questionNumber = 42,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/AminoacidsDB/leucina",
                "AnswerImages/AminoacidsDB/prolina",
                "AnswerImages/AminoacidsDB/triptofano",
                "AnswerImages/AminoacidsDB/asparagina"
            },
            correctIndex = 3,
            questionNumber = 43,
            isImageAnswer = true,
            isImageQuestion = true,
            questionImagePath = "AnswerImages/SugarDB/sugar_question_43",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/AminoacidsDB/leucina",
                "AnswerImages/AminoacidsDB/serina",
                "AnswerImages/AminoacidsDB/triptofano",
                "AnswerImages/AminoacidsDB/asparagina"
            },
            correctIndex = 1,
            questionNumber = 44,
            isImageAnswer = true,
            isImageQuestion = true,
            questionImagePath = "AnswerImages/SugarDB/sugar_question_44",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Os carboidratos são moléculas formadas, principalmente, por:",
            answers = new string[] {
                "Hidrogênio, enxofre e oxigênio",
                "Carbono, hidrogênio e oxigênio",
                "Carbono, nitrogênio e fósforo",
                "Oxigênio, fósforo e magnésio",
            },
            correctIndex = 1,
            questionNumber = 45,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "A fórmula geral mais comum dos monossacarídeos é:",
            answers = new string[] {
                "CnH2n-2On",
                "CnHnOn",
                "CnH2n+2On",
                "CnH2nOn"
            },
            correctIndex = 3,
            questionNumber = 46,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "A ligação que une os monossacarídeos para formar dissacarídeos e polissacarídeos chama-se:",
            answers = new string[] {
                "Ligação glicosídica",
                "Ligação peptídica",
                "Ligação dissulfeto",
                "Ligação fosfodiéster"
            },
            correctIndex = 0,
            questionNumber = 47,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Qual polissacarídeo é a principal reserva energética dos animais?",
            answers = new string[] {
                "Amido",
                "Quitina",
                "Glicogênio",
                "Celulose"
            },
            correctIndex = 2,
            questionNumber = 48,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "A quitina é um polissacarídeo estrutural encontrado em:",
            answers = new string[] {
                "Fígado humano",
                "Algas vermelhas",
                "Plantas superiores",
                "Parede celular de fungos e exoesqueleto de artrópodes"
            },
            correctIndex = 3,
            questionNumber = 49,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "A sacarose é formada pela união de quais monossacarídeos?",
            answers = new string[] {
                "Glicose + Frutose",
                "Glicose + Galactose",
                "Glicose + Glicose",
                "Frutose + Galactose"
            },
            correctIndex = 0,
            questionNumber = 50,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "A glicose, a frutose e a galactose são exemplos de:",
            answers = new string[] {
                "Oligossacarídeos",
                "Monossacarídeos",
                "Dissacarídeos",
                "Polissacarídeos"
            },
            correctIndex = 1,
            questionNumber = 51,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "A celulose difere do amido principalmente porque:",
            answers = new string[] {
                "É solúvel em água, enquanto o amido não é",
                "Possui ligações β(1→4), enquanto o amido possui α(1→4)",
                "É formada por frutose, enquanto o amido é formado por glicose",
                "É composta por galactose, e não glicosedeos"
            },
            correctIndex = 1,
            questionNumber = 52,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Qual das funções abaixo NÃO está relacionada aos carboidratos?",
            answers = new string[] {
                "Estrutura celular",
                "Fornecimento de energia",
                "Armazenamento de energia",
                "Catálise enzimática direta"
            },
            correctIndex = 3,
            questionNumber = 53,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Os carboidratos também são conhecidos como:",
            answers = new string[] {
                "Lípidos",
                "Glicídios",
                "Aminoácidos",
                "Nucleotídeos"
            },
            correctIndex = 1,
            questionNumber = 54,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "A fórmula geral mais comum dos monossacarídeos é:",
            answers = new string[] {
                "Cn(H₂O)n",
                "CnH₂nOn",
                "Cn(H₂O)2n",
                "CnHnOn"
            },
            correctIndex = 1,
            questionNumber = 55,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Qual dos seguintes é um monossacarídeo?",
            answers = new string[] {
                "Sacarose",
                "Maltose",
                "Glicose",
                "Amido"
            },
            correctIndex = 2,
            questionNumber = 56,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "A sacarose, encontrada no açúcar de mesa, é formada por:",
            answers = new string[] {
                "Glicose + Frutose",
                "Glicose + Galactose",
                "Frutose + Galactose",
                "Glicose + Glicose"
            },
            correctIndex = 0,
            questionNumber = 57,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "O amido é o principal polissacarídeo de reserva:",
            answers = new string[] {
                "Animal",
                "Fúngico",
                "Vegetal",
                "Bacteriano"
            },
            correctIndex = 2,
            questionNumber = 58,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "O glicogênio é o polissacarídeo de reserva característico de:",
            answers = new string[] {
                "Plantas",
                "Fungos e animais",
                "Bactérias",
                "Protozoários"
            },
            correctIndex = 1,
            questionNumber = 59,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "A celulose tem como função principal nos vegetais:",
            answers = new string[] {
                "Armazenar energia",
                "Dar resistência à parede celular",
                "Servir como reserva osmótica",
                "Participar da respiração celular"
            },
            correctIndex = 1,
            questionNumber = 60,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "A lactose, presente no leite, é formada por:",
            answers = new string[] {
                "Glicose + Glicose",
                "Glicose + Galactose",
                "Glicose + Frutose",
                "Galactose + Frutose"
            },
            correctIndex = 1,
            questionNumber = 61,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Entre as alternativas abaixo, qual é um polissacarídeo estrutural?",
            answers = new string[] {
                "Amido",
                "Glicogênio",
                "Celulose",
                "Maltose"
            },
            correctIndex = 2,
            questionNumber = 62,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "A fórmula geral mais comum dos carboidratos é:",
            answers = new string[] {
                "Cn(H₂O)n",
                "CnH₂nOn",
                "CnH₂n+₂On",
                "CnHnOn"
            },
            correctIndex = 0,
            questionNumber = 63,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "A glicose é classificada como:",
            answers = new string[] {
                "Dissacarídeo",
                "Monossacarídeo",
                "Polissacarídeo",
                "Oligossacarídeo"
            },
            correctIndex = 1,
            questionNumber = 64,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "A sacarose é formada pela união de:",
            answers = new string[] {
                "Glicose + Glicose",
                "Glicose + Galactose",
                "Glicose + Frutose",
                "Glicose + Manose"
            },
            correctIndex = 2,
            questionNumber = 65,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
         new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Qual destes carboidratos é considerado um polissacarídeo de armazenamento em animais?",
            answers = new string[] {
                "Amido",
                "Celulose",
                "Glicogênio",
                "Quitina"
            },
            correctIndex = 2,
            questionNumber = 66,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Qual destes carboidratos é um polissacarídeo estrutural presente na parede celular de vegetais?",
            answers = new string[] {
                "Amido",
                "Celulose",
                "Maltose",
                "Lactose"
            },
            correctIndex = 1,
            questionNumber = 67,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Qual destes carboidratos está presente no leite?",
            answers = new string[] {
                "Sacarose",
                "Lactose",
                "Maltose",
                "Glicogênio"
            },
            correctIndex = 1,
            questionNumber = 68,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Os carboidratos podem ser classificados de acordo com:",
            answers = new string[] {
                "O número de radicais R",
                "O número de átomos de carbono e grupos funcionais",
                "A presença de ácidos graxos",
                "O número de ligações peptídicas"
            },
            correctIndex = 1,
            questionNumber = 69,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "CarbohydratesQuestionDatabase",
            questionText = "Qual das opções representa uma função principal dos carboidratos nos organismos vivos?",
            answers = new string[] {
                "Atuar como catalisadores biológicos",
                "Armazenar informações genéticas",
                "Fornecer energia e servir como reserva energética",
                "Compor membranas celulares"
            },
            correctIndex = 2,
            questionNumber = 70,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        }
    };

    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.carbohydrates;
    public string GetDatabankName()  => "CarbohydratesQuestionDatabase";
    public string GetDisplayName()   => "Carboidratos";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;

}