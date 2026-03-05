using UnityEngine;
using System.Collections.Generic;
using QuestionSystem;

public class LipidsQuestionDatabase : MonoBehaviour, IQuestionDatabase
{

    [Header("Development Settings")]
    [SerializeField] private bool databaseInDevelopment = false;
    
    private List<Question> questions = new List<Question>
    {
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "O que são lipídios?",
            answers = new string[] {
                "Moléculas polares, que se associam através de interações eletrostáticas",
                "Moléculas apolares, que se associam através de interações de hidrogênio",
                "Moléculas apolares, que se associam através de interações de hidrofóbicas",
                "Moléculas polares, que se associam através da hidratação"
            },
            correctIndex = 2,
            questionNumber = 1,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os lipídeos são moléculas que apresentam uma grande variedade de estruturas, mas com uma propriedade física comum",
            answers = new string[] {
                "São totalmente apolares",
                "São totalmente polares",
                "São hidrofílicas",
                "São anfipáticas"
            },
            correctIndex = 3,
            questionNumber = 2,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "O termo lipofílico se refere a:",
            answers = new string[] {
                "Afinidade por água",
                "Afinidade por lipídios",
                "Afinidade por solventes polares",
                "Afinidade por altas temperaturas"
            },
            correctIndex = 1,
            questionNumber = 3,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "O termo hidrofóbico se refere a:",
            answers = new string[] {
                "Repulsão por água",
                "Afinidade por água",
                "Afinidade por solventes orgânicos",
                "Afinidade por altas temperaturas"
            },
            correctIndex = 0,
            questionNumber = 4,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Moléculas com regiões polares e apolares são chamadas:",
            answers = new string[] {
                "Hidrofílicas",
                "Hidrofóbicas",
                "Anfipáticas",
                "Apolares"
            },
            correctIndex = 2,
            questionNumber = 5,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os lipídeos mais simples são:",
            answers = new string[] {
                "Triglicerídeos",
                "Fosfolipídios",
                "Ácidos graxos",
                "Esteróides"
            },
            correctIndex = 2,
            questionNumber = 6,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "O que caracteriza um ácido graxo?",
            answers = new string[] {
                "Uma longa cadeia de hidrocarbonetos com um grupo carboxila.",
                "Um anel de hidrocarbonetos com um grupo amino.",
                "Uma cadeia curta de hidrocarbonetos com um grupo fosfato.",
                "Um açúcar com múltiplos grupos hidroxila."
            },
            correctIndex = 0,
            questionNumber = 7,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Indique abaixo o lipídeo mono-insaturado",
            answers = new string[] {
                "AnswerImages/LipidDB/acido_graxo_saturado",
                "AnswerImages/LipidDB/acido_graxo_mono_insaturado",
                "AnswerImages/LipidDB/acido_graxo_di_insaturado",
                "AnswerImages/LipidDB/acido_graxo_tri_insaturado"
            },
            correctIndex = 1,
            questionNumber = 8,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Ácidos graxos poli-insaturados possuem:",
            answers = new string[] {
                "Apenas ligações simples carbono-carbono.",
                "mais de uma ligação dupla carbono-carbono.",
                "uma ligação dupla carbono-carbono",
                "não possuem insaturações"
            },
            correctIndex = 1,
            questionNumber = 9,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os lipídeos se agrupam através de interações hidrofóbicas. Indique abaixo qual lipídeo possuirá interações mais fracas.",
            answers = new string[] {
                "AnswerImages/LipidDB/acido_graxo_saturado",
                "AnswerImages/LipidDB/acido_graxo_mono_insaturado",
                "AnswerImages/LipidDB/acido_graxo_di_insaturado",
                "AnswerImages/LipidDB/acido_graxo_tri_insaturado"
            },
            correctIndex = 3,
            questionNumber = 10,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Quais os dois fatores que afetam diretamente o ponto de fusão de lipídeos?",
            answers = new string[] {
                "densidade /tensão superficial",
                "grau de instaturação / polaridade",
                "tamanho da cadeia carbônica / grau de insaturação",
                "viscosidade / tamanho da cadeia carbônica."
            },
            correctIndex = 2,
            questionNumber = 11,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Ácidos graxos saturados geralmente são:",
            answers = new string[] {
                "Líquidos à temperatura ambiente.",
                "Sólidos à temperatura ambiente.",
                "Gasosos à temperatura ambiente.",
                "Insolúveis em solventes orgânicos."
            },
            correctIndex = 1,
            questionNumber = 12,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Ácidos graxos insaturados geralmente são:",
            answers = new string[] {
                "Líquidos à temperatura ambiente.",
                "Sólidos à temperatura ambiente.",
                "Gasosos à temperatura ambiente.",
                "Insolúveis em solventes orgânicos."
            },
            correctIndex = 0,
            questionNumber = 13,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Indique abaixo o lípideo com o MAIOR ponto de fusão",
            answers = new string[] {
                "AnswerImages/LipidDB/acido_graxo_saturado",
                "AnswerImages/LipidDB/acido_graxo_mono_insaturado",
                "AnswerImages/LipidDB/acido_graxo_di_insaturado",
                "AnswerImages/LipidDB/acido_graxo_tri_insaturado"
            },
            correctIndex = 0,
            questionNumber = 14,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Indique abaixo o lípideo com o MENOR ponto de fusão",
            answers = new string[] {
                "AnswerImages/LipidDB/acido_graxo_saturado",
                "AnswerImages/LipidDB/acido_graxo_mono_insaturado",
                "AnswerImages/LipidDB/acido_graxo_di_insaturado",
                "AnswerImages/LipidDB/acido_graxo_tri_insaturado"
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
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os lipídeos têm um sistema de nomenclatura e abreviações bem peculiar. Indique abaixo o lipídeo cuja abreviação é 18:2 <sup>∆9, 12</sup>",
            answers = new string[] {
                "AnswerImages/LipidDB/acido_graxo_saturado",
                "AnswerImages/LipidDB/acido_graxo_mono_insaturado",
                "AnswerImages/LipidDB/acido_graxo_di_insaturado",
                "AnswerImages/LipidDB/acido_graxo_tri_insaturado"
            },
            correctIndex = 2,
            questionNumber = 16,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Há um sistema de classificação que identifica os lipídeos através de sua extremidade ômega. Sendo assim, indique abaixo o lípideo que pertence a família ômega-3",
            answers = new string[] {
                "AnswerImages/LipidDB/colesterol",
                "AnswerImages/LipidDB/acido_graxo_di_insaturado",
                "AnswerImages/LipidDB/acido_graxo_mono_insaturado",
                "AnswerImages/LipidDB/acido_graxo_tri_insaturado"
            },
            correctIndex = 3,
            questionNumber = 17,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os triacilgliceróis são formados por:",
            answers = new string[] {
                "Três ácidos graxos e uma molécula de glicerol.",
                "Dois ácidos graxos e uma molécula de glicerol.",
                "Um ácido graxo e uma molécula de glicerol.",
                "Três ácidos graxos e duas moléculas de glicerol."
            },
            correctIndex = 0,
            questionNumber = 18,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Qual a principal função dos triacilgliceróis no organismo?",
            answers = new string[] {
                "Formar membranas celulares.",
                "Armazenar energia.",
                "Sintetizar hormônios.",
                "Transportar oxigênio."
            },
            correctIndex = 1,
            questionNumber = 19,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os triacilgliceróis são armazenados principalmente em:",
            answers = new string[] {
                "Fígado",
                "Músculos",
                "Cérebro",
                "Células adiposas"
            },
            correctIndex = 3,
            questionNumber = 20,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "O tecido adiposo tem como função:",
            answers = new string[] {
                "Armazenar gordura.",
                "Isolar o organismo.",
                "Proteger órgãos.",
                "Todas as alternativas anteriores."
            },
            correctIndex = 3,
            questionNumber = 21,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "A hibernação é uma estratégia de sobrevivência que envolve:",
            answers = new string[] {
                "Aumento do consumo de oxigênio.",
                "Diminuição do consumo de oxigênio.",
                "Armazenamento de lipídeos.",
                "Aumento da atividade enzimática."
            },
            correctIndex = 2,
            questionNumber = 22,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Em comparação com carboidratos e proteínas, os triacilgliceróis armazenam:",
            answers = new string[] {
                "Menor quantidade de energia por grama.",
                "Maior quantidade de energia por grama.",
                "A mesma quantidade de energia por grama.",
                "Não armazenam energia."
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
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os ácidos graxos essenciais são aqueles que:",
            answers = new string[] {
                "O organismo produz em grande quantidade.",
                "O organismo não consegue sintetizar.",
                "São encontrados apenas em animais.",
                "São encontrados apenas em plantas."
            },
            correctIndex = 1,
            questionNumber = 24,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Exemplos de ácidos graxos essenciais são:",
            answers = new string[] {
                "Ácido esteárico e ácido palmítico.",
                "Ácido linoléico e ácido linolênico.",
                "Ácido oléico e ácido palmitoléico.",
                "Ácido araquidônico e ácido eicosapentaenóico."
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
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "O ácido linoléico (ômega-6) é precursor de:",
            answers = new string[] {
                "Prostaglandinas e tromboxanas.",
                "Vitamina D.",
                "Colesterol",
                "Glicogênio"
            },
            correctIndex = 0,
            questionNumber = 26,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "O desenvolvimento cerebral.",
                "A função imunológica.",
                "A saúde da retina.",
                "Todas as alternativas anteriores."
            },
            correctIndex = 3,
            questionNumber = 27,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/LipidDB/lipids_question_27",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "A deficiência de ácidos graxos essenciais pode causar:",
            answers = new string[] {
                "Dermatite.",
                "Problemas neurológicos.",
                "Problemas no desenvolvimento de bebês.",
                "Todas as alternativas anteriores."
            },
            correctIndex = 3,
            questionNumber = 28,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Identifique abaixo o ácido graxo na conformação trans.",
            answers = new string[] {
                "AnswerImages/LipidDB/colesterol",
                "AnswerImages/LipidDB/acido_graxo_di_insaturado",
                "AnswerImages/LipidDB/acido_graxo_mono_insaturado",
                "AnswerImages/LipidDB/acido_graxo_trans"
            },
            correctIndex = 3,
            questionNumber = 29,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "O consumo de ácidos graxos trans está associado a:",
            answers = new string[] {
                "Diminuição do risco de doenças cardíacas.",
                "Aumento do risco de doenças cardíacas.",
                "Nenhuma alteração no risco de doenças cardíacas.",
                "Aumento da produção de HDL."
            },
            correctIndex = 1,
            questionNumber = 30,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "A margarina é um composto:",
            answers = new string[] {
                "Natural, composto somente por ácidos graxos saturados.",
                "Artificial, composto somente por ácidos graxos insaturados.",
                "Artificial, composto por ácidos graxos saturados e insaturados.",
                "Natural, composto somente por ácidos graxos trans."
            },
            correctIndex = 2,
            questionNumber = 31,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "A hidrogenação de óleos vegetais resulta em:",
            answers = new string[] {
                "Aumento do grau de insaturação.",
                "Diminuição cadeia carbônica.",
                "Aumento do ponto de fusão.",
                "Diminuição do ponto de fusão."
            },
            correctIndex = 2,
            questionNumber = 32,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Reação de neutralização",
                "Saponificação",
                "Acilação",
                "Esterificação"
            },
            correctIndex = 1,
            questionNumber = 33,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/LipidDB/lipids_question_33",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Transesterificação",
                "Saponificação",
                "Acilação",
                "Esterificação"
            },
            correctIndex = 0,
            questionNumber = 34,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/LipidDB/lipids_question_34",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Óleo de cozinha",
                "Lubrificante",
                "Biodiesel",
                "Detergente"
            },
            correctIndex = 3,
            questionNumber = 35,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/LipidDB/lipids_question_35",
            questionLevel = 2,
            questionInDevelopment = true
        },
         new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Óleo de cozinha",
                "Lubrificante",
                "Biodiesel",
                "Detergente"
            },
            correctIndex = 2,
            questionNumber = 36,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/LipidDB/lipids_question_36",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os esteróis são a terceira maior classe de lipídeos encontrados em membranas celulares. O principal deles é o colesterol. Qual é a estrutura do colesterol?",
            answers = new string[] {
                "AnswerImages/LipidDB/acido_graxo_tri_insaturado",
                "AnswerImages/LipidDB/esterol",
                "AnswerImages/LipidDB/fosfatidilcolina",
                "AnswerImages/LipidDB/colesterol"
            },
            correctIndex = 3,
            questionNumber = 37,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "O colesterol tem como função:",
            answers = new string[] {
                "Formar membranas celulares.",
                "Ser precursor de hormônios.",
                "Ser precursor de sais biliares.",
                "Todas as alternativas anteriores."
            },
            correctIndex = 3,
            questionNumber = 38,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Qual a importância do colesterol para a membrana celular?",
            answers = new string[] {
                "Confere maior rigidez a membrana celular",
                "Forma sítios hidrofílicos no meio da membrana celular",
                "Atuam interagindo com a água",
                "Introduz insaturações do tipo trans na membrana celular."
            },
            correctIndex = 0,
            questionNumber = 39,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Ácidos graxos insaturados são encontrados principalmente em:",
            answers = new string[] {
                "Gorduras animais.",
                "Óleos vegetais.",
                "Cereais.",
                "Leguminosas."
            },
            correctIndex = 1,
            questionNumber = 40,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "O que é um ácido graxo monoinsaturado?",
            answers = new string[] {
                "Um ácido graxo com uma dupla ligação.",
                "Um ácido graxo com múltiplas ligações duplas.",
                "Um ácido graxo saturado.",
                "Um ácido graxo com um grupo amino."
            },
            correctIndex = 0,
            questionNumber = 41,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "O que é um ácido graxo poliinsaturado?",
            answers = new string[] {
                "Um ácido graxo saturado.",
                "Um ácido graxo com uma dupla ligação.",
                "Um ácido graxo com múltiplas ligações duplas.",
                "Um ácido graxo com um grupo fosfato."
            },
            correctIndex = 2,
            questionNumber = 42,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "A configuração cis e trans em ácidos graxos se refere a:",
            answers = new string[] {
                "O comprimento da cadeia.",
                "O grau de saturação.",
                "A posição das duplas ligações.",
                "A orientação dos grupamentos ao redor de uma ligação dupla."
            },
            correctIndex = 3,
            questionNumber = 43,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os lipídeos são, em sua maioria, compostos por:",
            answers = new string[] {
                "Nitrogênio e fósforo",
                "Carbono e enxofre",
                "Carbono, hidrogênio e oxigênio",
                "Oxigênio e cloro"
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
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Qual das funções abaixo é típica dos lipídeos?",
            answers = new string[] {
                "Transportar oxigênio",
                "Carregar informações genéticas",
                "Catalisar reações químicas",
                "Armazenar energia"
            },
            correctIndex = 3,
            questionNumber = 45,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os lipídeos insolúveis em água são considerados:",
            answers = new string[] {
                "Hidrofílicos",
                "Hidrofóbicos",
                "Anfipáticos",
                "Polares"
            },
            correctIndex = 1,
            questionNumber = 46,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Qual lipídeo é o principal componente das membranas celulares?",
            answers = new string[] {
                "Fosfolipídios",
                "Triglicerídeos",
                "Cerídeos",
                "Esteroides"
            },
            correctIndex = 0,
            questionNumber = 47,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os triglicerídeos são formados por:",
            answers = new string[] {
                "Ácidos graxos e glicerol",
                "Triglicerídeos",
                "Nucleotídeos e açúcar",
                "Glicerol e bases nitrogenadas"
            },
            correctIndex = 0,
            questionNumber = 48,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os triglicerídeos são formados por:",
            answers = new string[] {
                "Ácidos graxos e glicerol",
                "Triglicerídeos",
                "Nucleotídeos e açúcar",
                "Glicerol e bases nitrogenadas"
            },
            correctIndex = 0,
            questionNumber = 49,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Qual lipídeo atua como precursor dos hormônios esteroides?",
            answers = new string[] {
                "Lecitina",
                "Colesterol",
                "Ácido oleico",
                "Cerídeos"
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
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os ácidos graxos insaturados diferem dos saturados por:",
            answers = new string[] {
                "Terem oxigênio extra",
                "Colesterol",
                "Ácido oleico",
                "Não possuírem carbono"
            },
            correctIndex = 1,
            questionNumber = 51,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os ácidos graxos insaturados diferem dos saturados por:",
            answers = new string[] {
                "Terem oxigênio extra",
                "Colesterol",
                "Ácido oleico",
                "Não possuírem carbono"
            },
            correctIndex = 1,
            questionNumber = 52,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os ácidos graxos insaturados diferem dos saturados por:",
            answers = new string[] {
                "Terem oxigênio extra",
                "Colesterol",
                "Estarem sempre sólidos à temperatura ambiente",
                "Apresentarem ligações duplas na cadeia carbônica"
            },
            correctIndex = 3,
            questionNumber = 53,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Um exemplo de lipídeo de reserva energética encontrado em animais é:",
            answers = new string[] {
                "Glicogênio",
                "Colesterol",
                "Triglicerídeo",
                "Fosfolipídio"
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
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os lipídeos que apresentam regiões polares e apolares são chamados de:",
            answers = new string[] {
                "Hidrofílicos",
                "Hidrofóbicos",
                "Aromáticos",
                "Anfipáticos"
            },
            correctIndex = 3,
            questionNumber = 55,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "A função dos fosfolipídios na membrana celular é principalmente:",
            answers = new string[] {
                "Produzir energia imediata",
                "Armazenar glicose",
                "Formar a bicamada lipídica",
                "Catalisar reações químicas"
            },
            correctIndex = 2,
            questionNumber = 56,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os lipídeos são principalmente compostos por:",
            answers = new string[] {
                "Aminoácidos e nucleotídeos",
                "Glicerol e ácidos graxos",
                "Monossacarídeos e polissacarídeos",
                "Peptídeos e cofatores"
            },
            correctIndex = 1,
            questionNumber = 57,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Qual é a principal função dos lipídeos de reserva nos animais?",
            answers = new string[] {
                "Armazenar informações genéticas",
                "Catalisar reações químicas",
                "Armazenar energia a longo prazo",
                "Transportar oxigênio"
            },
            correctIndex = 2,
            questionNumber = 58,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "O colesterol é classificado como:",
            answers = new string[] {
                "Esteroide",
                "Fosfolipídeo",
                "Glicerídeo",
                "Terpeno"
            },
            correctIndex = 0,
            questionNumber = 59,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os fosfolipídeos são fundamentais porque:",
            answers = new string[] {
                "Atuam como catalisadores",
                "Formam a bicamada das membranas celulares",
                "São hormônios sexuais",
                "Fornecem energia imediata"
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
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "O que diferencia ácidos graxos saturados de insaturados?",
            answers = new string[] {
                "Presença ou ausência de grupo carboxila",
                "Quantidade de átomos de oxigênio",
                "Presença de ligações duplas entre carbonos",
                "Presença de fósforo na cadeia"
            },
            correctIndex = 2,
            questionNumber = 61,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os óleos vegetais, em temperatura ambiente, geralmente são:",
            answers = new string[] {
                "Sólidos, pois são saturados",
                "Líquidos, pois são insaturados",
                "Gasosos, pois são voláteis",
                "Sólidos, pois contêm esteroides"
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
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os lipídeos são moléculas:",
            answers = new string[] {
                "Hidrossolúveis",
                "Insolúveis em água e solúveis em solventes orgânicos",
                "Sempre solúveis em água",
                "Exclusivamente polares"
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
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os lipídeos são moléculas:",
            answers = new string[] {
                "Hidrossolúveis",
                "Insolúveis em água e solúveis em solventes orgânicos",
                "Sempre solúveis em água",
                "Exclusivamente polares"
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
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os hormônios sexuais (como testosterona e estrógeno) derivam de qual lipídeo?",
            answers = new string[] {
                "Fosfolipídeos",
                "Colesterol",
                "Glicerídeos",
                "Carotenoides"
            },
            correctIndex = 1,
            questionNumber = 65,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "A principal função dos lipídeos na membrana plasmática é:",
            answers = new string[] {
                "Armazenar glicose",
                "Regular a temperatura do corpo",
                "Garantir a barreira seletiva e a fluidez da membrana",
                "Fornecer aminoácidos essenciais"
            },
            correctIndex = 2,
            questionNumber = 66,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "A manteiga é rica em ácidos graxos:",
            answers = new string[] {
                "Insaturados, geralmente líquidos à temperatura ambiente",
                "Saturados, geralmente sólidos à temperatura ambiente",
                "Aromáticos, voláteis",
                "Fosforilados, presentes em membranas"
            },
            correctIndex = 1,
            questionNumber = 67,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os lipídios são compostos orgânicos caracterizados principalmente por:",
            answers = new string[] {
                "Alta solubilidade em água",
                "Baixa solubilidade em água e solubilidade em solventes orgânicos",
                "Estrutura formada por nucleotídeos",
                "Sempre possuírem função enzimática"
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
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os principais componentes de óleos e gorduras são:",
            answers = new string[] {
                "Fosfolipídios",
                "Glicídios",
                "Triglicerídeos",
                "Esteroides"
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
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Um lipídio formado por glicerol + 3 ácidos graxos é denominado:",
            answers = new string[] {
                "Fosfolipídio",
                "Esteroide",
                "Triglicerídeo",
                "Cerídeo"
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
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os fosfolipídios são importantes porque:",
            answers = new string[] {
                "Formam a parede celular dos vegetais",
                "Atuam como catalisadores",
                "Compõem a membrana plasmática das células",
                "São responsáveis pelo transporte de oxigênio"
            },
            correctIndex = 2,
            questionNumber = 71,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Qual dos lipídios abaixo possui função hormonal?",
            answers = new string[] {
                "Triglicerídeos",
                "Esteroides",
                "Fosfolipídios",
                "Cerídeos"
            },
            correctIndex = 1,
            questionNumber = 72,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "A principal função dos lipídios de reserva é:",
            answers = new string[] {
                "Fornecer energia de curto prazo",
                "Armazenar energia de longo prazo",
                "Atuar como cofatores enzimáticos",
                "Regular o pH celular"
            },
            correctIndex = 1,
            questionNumber = 73,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os lipídios que atuam como isolantes térmicos em animais são principalmente:",
            answers = new string[] {
                "Fosfolipídios",
                "Esteroides",
                "Glicídios",
                "Triglicerídeos"
            },
            correctIndex = 3,
            questionNumber = 74,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Um exemplo de cera (cerídeo) é:",
            answers = new string[] {
                "Colesterol",
                "Cutina das folhas",
                "Fosfatidilcolina",
                "Amido"
            },
            correctIndex = 1,
            questionNumber = 75,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os ácidos graxos insaturados diferem dos saturados porque:",
            answers = new string[] {
                "Possuem cadeias ramificadas",
                "Apresentam uma ou mais duplas ligações na cadeia carbônica",
                "Não contêm hidrogênio em sua estrutura",
                "São encontrados apenas em animais"
            },
            correctIndex = 1,
            questionNumber = 76,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        }
    };

    public List<Question> GetQuestions()
    {
        return questions;
    }

    public QuestionSet GetQuestionSetType()
    {
        return QuestionSet.lipids;
    }

    public string GetDatabankName()
    {
        return "LipidsQuestionDatabase";
    }

    public bool IsDatabaseInDevelopment()
    {
        return databaseInDevelopment;
    }
}