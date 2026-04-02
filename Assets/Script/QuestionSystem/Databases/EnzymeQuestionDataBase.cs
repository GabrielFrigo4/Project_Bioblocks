using UnityEngine;
using System.Collections.Generic;
using QuestionSystem;

public class EnzymeQuestionDatabase : MonoBehaviour, IQuestionDatabase
{

    [Header("Development Settings")]
    [SerializeField] private bool databaseInDevelopment = false;
    
    private List<Question> questions = new List<Question>
    {
       new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O que são enzimas?",
            answers = new string[] {
                "Catalisadores químicos inorgânicos.",
                "Catalisadores biológicos, principalmente proteínas.",
                "Substratos que participam de reações químicas.",
                "Produtos de reações químicas."
            },
            correctIndex = 1,
            questionNumber = 1,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Qual a principal função de uma enzima?",
            answers = new string[] {
                "Sintetizar proteínas.",
                "Aumentar a velocidade de uma reação.",
                "Regular a temperatura corporal.",
                "Transportar oxigênio."
            },
            correctIndex = 1,
            questionNumber = 2,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Como as enzimas aumentam a velocidade das reações?",
            answers = new string[] {
                "Aumentando a energia de ativação.",
                "Diminuindo a energia de ativação.",
                "Alterando o equilíbrio da reação.",
                "Aumentando a concentração de substrato."
            },
            correctIndex = 1,
            questionNumber = 3,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O que é o estado de transição em uma reação?",
            answers = new string[] {
                "O estado inicial da reação.",
                "O estado final da reação.",
                "Um estado intermediário de alta energia.",
                "Um catalisador."
            },
            correctIndex = 2,
            questionNumber = 4,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O que é energia de ativação?",
            answers = new string[] {
                "A energia necessária para iniciar uma reação.",
                "A energia liberada durante uma reação.",
                "A diferença de energia entre o substrato e o estado de transição.",
                "A energia do produto."
            },
            correctIndex = 2,
            questionNumber = 5,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "As enzimas atuam em condições:",
            answers = new string[] {
                "Extremas de temperatura e pH.",
                "Compatíveis com a vida.",
                "Exclusivamente in vitro.",
                "Independentes do meio."
            },
            correctIndex = 1,
            questionNumber = 6,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O sítio ativo de uma enzima é:",
            answers = new string[] {
                "A região onde a enzima se liga ao produto.",
                "A região onde a enzima se liga ao substrato.",
                "A região responsável pela regulação da enzima.",
                "A região onde a enzima se liga a cofatores."
            },
            correctIndex = 1,
            questionNumber = 7,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O modelo chave-fechadura descreve a interação enzima-substrato como:",
            answers = new string[] {
                "Um ajuste induzido.",
                "Uma ligação covalente.",
                "Um encaixe complementar.",
                "Uma interação hidrofóbica."
            },
            correctIndex = 2,
            questionNumber = 8,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Qual fator é essencial para que uma enzima exerça sua ativiade plenamente",
            answers = new string[] {
                "A sua estrutura primária",
                "A estabilidade de sua estrutura terciária",
                "A quantidade de alfa-hélices na estrutura da enzima",
                "A formação de estrutura quaternária"
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
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Por que enzimas podem ser usadas na indústria",
            answers = new string[] {
                "Reação enzimática ocorre em temperaturas brandas.",
                "Enzimas são altamente específicas.",
                "Necessita-se de quantidades bem pequenas de enzimas, mesmo em escala industrial.",
                "Todas as alternativas são corretas."
            },
            correctIndex = 3,
            questionNumber = 10,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "As enzimas podem ser agrupadas em seis grandes grupos, de acordo com o tipo de reação que ela catalisa. Abaixo temos alguns nome de grupos de enzimas, exceto: ",
            answers = new string[] {
                "Hidrolases",
                "Ribolase",
                "Oxidoredutases",
                "Liases"
            },
            correctIndex = 1,
            questionNumber = 11,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Região da enzima responsável por interagir com a água",
                "Região da enzima com grande afinidade por íons",
                "Região da enzima que participa diretamente da catálise",
                "Região da enzima altamente hidrofóbica"
            },
            correctIndex = 2,
            questionNumber = 12,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/EnzymeDB/enzymeDB_ImageQuestionContainer12",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A atividade de uma enzima pode ser afetada por:",
            answers = new string[] {
                "Temperatura e pH.",
                "Concentração de substrato.",
                "Presença de inibidores.",
                "Todas as alternativas anteriores."
            },
            correctIndex = 3,
            questionNumber = 13,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O pH ótimo de uma enzima é:",
            answers = new string[] {
                "O pH em que a enzima tem atividade máxima.",
                "O pH em que a enzima é inativada.",
                "O pH em que a enzima é desnaturada.",
                "O pH do meio celular."
            },
            correctIndex = 0,
            questionNumber = 14,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A temperatura ótima de uma enzima é:",
            answers = new string[] {
                "A temperatura em que a enzima é desnaturada.",
                "A temperatura em que a enzima tem atividade máxima.",
                "A temperatura ambiente.",
                "A temperatura do organismo."
            },
            correctIndex = 1,
            questionNumber = 15,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O que acontece com a atividade de uma enzima quando a temperatura aumenta muito além da sua temperatura ótima?",
            answers = new string[] {
                "Aumenta.",
                "Diminui.",
                "Permanece constante.",
                "Varia de forma imprevisível."
            },
            correctIndex = 1,
            questionNumber = 16,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Desnaturação de uma enzima significa:",
            answers = new string[] {
                "Ativação da enzima.",
                "Perda da atividade enzimática devido à alteração da sua estrutura.",
                "Aumento da velocidade da reação.",
                "Formação de um complexo enzima-substrato."
            },
            correctIndex = 1,
            questionNumber = 17,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Quais fatores podem causar a desnaturação de uma enzima?",
            answers = new string[] {
                "Altas temperaturas.",
                "Variações de pH.",
                "Solventes orgânicos.",
                "Todas as alternativas anteriores."
            },
            correctIndex = 3,
            questionNumber = 18,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Inibidores enzimáticos são moléculas que:",
            answers = new string[] {
                "Aumentam a atividade da enzima.",
                "Diminuem ou impedem a atividade da enzima.",
                "Alteram o equilíbrio da reação.",
                "São substratos da enzima."
            },
            correctIndex = 1,
            questionNumber = 19,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
         new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Inibidores irreversíveis se ligam à enzima:",
            answers = new string[] {
                "Reversivelmente.",
                "Irreversivelmente, modificando permanentemente sua estrutura.",
                "Em um sítio alostérico.",
                "Somente em pH ácido."
            },
            correctIndex = 1,
            questionNumber = 20,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
       new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Inibição Irreversível",
                "Inibição Competitiva",
                "Inibição  Não-Competitiva",
                "Inibição A-Competitiva"
            },
            correctIndex = 2,
            questionNumber = 21,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/EnzymeDB/enzymeDB_ImageQuestionContainer21",
            questionLevel = 3,
            questionInDevelopment = false
        },
       new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Inibição Irreversível",
                "Inibição Competitiva",
                "Inibição  Não-Competitiva",
                "Inibição A-Competitiva"
            },
            correctIndex = 1,
            questionNumber = 22,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/EnzymeDB/enzymeDB_ImageQuestionContainer22",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A constante de Michaelis (Km) indica:",
            answers = new string[] {
                "A velocidade máxima da reação.",
                "A concentração de enzima.",
                "A concentração de substrato necessária para a enzima atingir metade da sua velocidade máxima.",
                "A energia de ativação."
            },
            correctIndex = 2,
            questionNumber = 23,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Um Km baixo indica:",
            answers = new string[] {
                "Baixa interação da enzima com substrato.",
                "Alta interação da enzima com substrato.",
                "Velocidade máxima de reação baixa.",
                "Velocidade máxima de reação alta."
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
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Um Km alto indica:",
            answers = new string[] {
                "Baixa interação da enzima com substrato.",
                "Alta interação da enzima com substrato.",
                "Velocidade máxima de reação alta.",
                "Velocidade máxima de reação baixa."
            },
            correctIndex = 0,
            questionNumber = 25,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A equação de Michaelis-Menten relaciona:",
            answers = new string[] {
                "Km, Vmax e a concentração de substrato.",
                "KM, pH e temperatura.",
                "Vmax, temperatura e pH.",
                "KM, pKa e a concentração de substrato."
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
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Na equação de Michaelis-Menten, Vmax representa:",
            answers = new string[] {
                "A velocidade inicial da reação.",
                "A velocidade máxima da reação.",
                "A constante de Michaelis.",
                "A concentração de substrato."
            },
            correctIndex = 1,
            questionNumber = 27,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
       new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Gráfico de Michaelis-Menten",
                "Gráfico Enzimático",
                "Gráfico de Lineweaver-Burk",
                "Gráfico Competitivo"
            },
            correctIndex = 2,
            questionNumber = 28,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/EnzymeDB/enzymeDB_ImageQuestionContainer28",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A enzima que hidrolisa o RNA é:",
            answers = new string[] {
                "DNA polimerase",
                "RNA polimerase",
                "Ribonuclease",
                "Protease"
            },
            correctIndex = 2,
            questionNumber = 29,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A enzima que hidrolisa proteínas é:",
            answers = new string[] {
                "Ribonuclease",
                "Protease",
                "Lipase",
                "Amílase"
            },
            correctIndex = 1,
            questionNumber = 30,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A enzima que hidrolisa lipídios é:",
            answers = new string[] {
                "Amílase",
                "Protease",
                "Lipase",
                "Ribonuclease"
            },
            correctIndex = 2,
            questionNumber = 31,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A enzima que hidrolisa amido é:",
            answers = new string[] {
                "Lipase",
                "Protease",
                "Amílase",
                "Ribonuclease"
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
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A pepsina é uma enzima:",
            answers = new string[] {
                "Que hidrolisa carboidratos.",
                "Que hidrolisa proteínas.",
                "Que hidrolisa lipídios.",
                "Que hidrolisa ácidos nucléicos."
            },
            correctIndex = 1,
            questionNumber = 33,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A pepsina atua melhor em qual pH?",
            answers = new string[] {
                "pH 7",
                "pH 10",
                "pH 2",
                "pH 14"
            },
            correctIndex = 2,
            questionNumber = 34,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A quimotripsina é uma enzima:",
            answers = new string[] {
                "Que hidrolisa carboidratos.",
                "Que hidrolisa proteínas.",
                "Que hidrolisa lipídios.",
                "Que hidrolisa ácidos nucléicos."
            },
            correctIndex = 1,
            questionNumber = 35,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A quimotripsina atua melhor em qual pH?",
            answers = new string[] {
                "pH 2",
                "pH 7",
                "pH 8",
                "pH 14"
            },
            correctIndex = 2,
            questionNumber = 36,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A amilase salivar é uma enzima:",
            answers = new string[] {
                "Que hidrolisa lipídios.",
                "Que hidrolisa proteínas.",
                "Que hidrolisa carboidratos.",
                "Que hidrolisa ácidos nucléicos."
            },
            correctIndex = 2,
            questionNumber = 37,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A amilase salivar atua melhor em qual pH?",
            answers = new string[] {
                "pH 2",
                "pH 7",
                "pH 8",
                "pH 14"
            },
            correctIndex = 1,
            questionNumber = 38,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A inibição enzimática irreversível causa:",
            answers = new string[] {
                "Uma diminuição temporária da atividade enzimática.",
                "Uma diminuição permanente da atividade enzimática.",
                "Um aumento da atividade enzimática.",
                "Nenhuma alteração na atividade enzimática."
            },
            correctIndex = 1,
            questionNumber = 39,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A inibição enzimática reversível competitiva pode ser superada por:",
            answers = new string[] {
                "Aumento da concentração do inibidor.",
                "Diminuição da concentração do inibidor.",
                "Aumento da concentração do substrato.",
                "Diminuição da concentração do substrato."
            },
            correctIndex = 2,
            questionNumber = 40,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A inibição enzimática reversível não-competitiva pode ser superada por:",
            answers = new string[] {
                "Aumento da concentração do substrato.",
                "Diminuição da concentração do substrato.",
                "Aumento da concentração do inibidor.",
                "Não pode ser superada."
            },
            correctIndex = 3,
            questionNumber = 41,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O captopril e o enalapril inibem a enzima:",
            answers = new string[] {
                "Ciclooxigenase",
                "ECA (enzima conversora de angiotensina)",
                "Lipase",
                "Protease"
            },
            correctIndex = 1,
            questionNumber = 42,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "As enzimas são classificadas como:",
            answers = new string[] {
                "Proteínas",
                "Carboidratos",
                "Vitaminas",
                "Lipídios"
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
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Qual é a principal função das enzimas no metabolismo celular?",
            answers = new string[] {
                "Transportar oxigênio",
                "Armazenar energia",
                "Produzir hormônios",
                "Acelerar reações químicas"
            },
            correctIndex = 3,
            questionNumber = 44,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        }, 
         new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O local da enzima onde o substrato se liga é chamado de:",
            answers = new string[] {
                "Sítio ativo",
                "Cofator",
                "Produto",
                "Cofator enzimático"
            },
            correctIndex = 0,
            questionNumber = 45,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Qual destes fatores pode alterar a atividade enzimática?",
            answers = new string[] {
                "Cor da enzima",
                "Pressão osmótica",
                "Temperatura e pH",
                "Massa molecular"
            },
            correctIndex = 2,
            questionNumber = 46,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        }, 
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Quando uma enzima perde sua estrutura tridimensional devido a altas temperaturas, esse processo é chamado:",
            answers = new string[] {
                "Redução",
                "Fusão",
                "Oxidação",
                "Desnaturação"
            },
            correctIndex = 3,
            questionNumber = 47,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        }, 
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A energia mínima necessária para iniciar uma reação química é chamada de:",
            answers = new string[] {
                "Energia solar",
                "nergia cinética",
                "Energia de ativação",
                "Energia potencial"
            },
            correctIndex = 2,
            questionNumber = 48,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        }, 
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Quando uma molécula semelhante ao substrato compete pelo sítio ativo da enzima, temos:",
            answers = new string[] {
                "Ativação enzimática",
                "Inibição não-competitiva",
                "Inibição competitiva",
                "Regulação alostérica"
            },
            correctIndex = 2,
            questionNumber = 49,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "As enzimas atuam de forma mais eficiente em:",
            answers = new string[] {
                "Temperatura e pH ótimos",
                "Qualquer temperatura ou pH",
                "Ambiente sem água",
                "Altas pressões"
            },
            correctIndex = 0,
            questionNumber = 50,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A região da enzima onde o substrato se liga é chamada de:",
            answers = new string[] {
                "Cofator",
                "Sítio ativo",
                "Centro metabólico",
                "Núcleo catalítico"
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
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O modelo chave-fechadura é usado para explicar:",
            answers = new string[] {
                "A especificidade entre enzima e substrato",
                "O armazenamento de energia na célula",
                "A formação de polissacarídeos",
                "A síntese de proteínas"
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
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Qual desses fatores não influencia a atividade enzimática?",
            answers = new string[] {
                "Temperatura",
                "pH",
                "Concentração de substrato",
                "Cor da solução"
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
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Coenzimas são:",
            answers = new string[] {
                "Íons metálicos que ajudam as enzimas",
                "Moléculas orgânicas auxiliares, muitas vezes derivadas de vitaminas",
                "Aminoácidos que formam o sítio ativo",
                "Produtos da reação enzimática"
            },
            correctIndex = 1,
            questionNumber = 54,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },    
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "As enzimas aceleram as reações químicas porque:",
            answers = new string[] {
                "Aumentam a energia de ativação",
                "Diminuem a energia de ativação",
                "Fornecem calor à reação",
                "Transformam substratos em vitaminas"
            },
            correctIndex = 1,
            questionNumber = 55,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },   
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A enzima que catalisa a quebra de amido em maltose é:",
            answers = new string[] {
                "Lactase",
                "Amilase",
                "Lipase",
                "Protease"
            },
            correctIndex = 1,
            questionNumber = 56,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },    
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A urease, enzima que degrada ureia, foi a primeira enzima cristalizada. Isso demonstrou que:",
            answers = new string[] {
                "Todas as enzimas são carboidratos",
                "Enzimas são proteínas",
                "Enzimas não podem ser isoladas",
                "Enzimas são apenas cofatores minerais"
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
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O inibidor competitivo atua:",
            answers = new string[] {
                "Ligando-se a um local diferente do sítio ativo",
                "Alterando irreversivelmente a enzima",
                "Compete com o substrato pelo sítio ativo",
                "Aumentando a afinidade da enzima pelo substrato"
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
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A função principal das enzimas é:",
            answers = new string[] {
                "Armazenar energia",
                "Acelerar reações químicas",
                "Servir como estrutura da célula",
                "Transportar oxigênio"
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
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O local da enzima onde o substrato se liga é chamado de:",
            answers = new string[] {
                "Cofator",
                "Sítio ativo",
                "Grupo prostético",
                "Complexo enzimático"
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
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O modelo que explica a interação enzima-substrato como chave-fechadura é conhecido como:",
            answers = new string[] {
                "Modelo do encaixe induzido",
                "Modelo da catálise covalente",
                "Modelo chave-fechadura",
                "Modelo do estado de transição"
            },
            correctIndex = 2,
            questionNumber = 61,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Qual dos fatores abaixo não afeta a atividade enzimática?",
            answers = new string[] {
                "Temperatura",
                "pH",
                "Concentração de substrato",
                "Cor do substrato"
            },
            correctIndex = 3,
            questionNumber = 62,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Moléculas não proteicas que auxiliam algumas enzimas em sua atividade são chamadas de:",
            answers = new string[] {
                "Cofatores",
                "Polissacarídeos",
                "Hormônios",
                "Nucleotídeos"
            },
            correctIndex = 0,
            questionNumber = 63,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A enzima que catalisa a quebra de moléculas pela adição de água é:",
            answers = new string[] {
                "Oxidorredutase",
                "Hidrolase",
                "Isomerase",
                "Ligase"
            },
            correctIndex = 1,
            questionNumber = 64,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A desnaturalização de uma enzima ocorre quando:",
            answers = new string[] {
                "A enzima é ativada por cofatores",
                "Há alteração em sua estrutura tridimensional",
                "O substrato se liga ao sítio ativo",
                "O pH se mantém constante"
            },
            correctIndex = 1,
            questionNumber = 65,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "As enzimas reduzem:",
            answers = new string[] {
                "A quantidade de solvente",
                "A energia de ativação da reação",
                "A quantidade de produtos formados",
                "A velocidade da reação"
            },
            correctIndex = 1,
            questionNumber = 66,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "As enzimas apresentam elevada:",
            answers = new string[] {
                "Generalidade",
                "Inespecificidade",
                "Especificidade",
                "Toxicidade"
            },
            correctIndex = 2,
            questionNumber = 67,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        }
    };
    
    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.enzymes;
    public string GetDatabankName()  => "EnzymeQuestionDatabase";
    public string GetDisplayName()   => "Enzimas";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;
}
