using UnityEngine;
using System.Collections.Generic;
using QuestionSystem;

public class NucleicAcidsQuestionDatabase : MonoBehaviour, IQuestionDatabase
{

    [Header("Development Settings")]
    [SerializeField] private bool databaseInDevelopment = false;
    
    private List<Question> questions = new List<Question>
    {
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Quem primeiro isolou o ácido nucléico?",
            answers = new string[] { "Watson", "Crick", "Friedrich Miescher", "Chargaff" },
            correctIndex = 2,
            questionNumber = 1,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual a principal função do RNA na célula?",
            answers = new string[] { 
                "Armazenamento de informação genética", 
                "Síntese de proteínas", 
                "Catálise de reações", 
                "Transporte de íons" 
            },
            correctIndex = 1,
            questionNumber = 2,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Quais são os três componentes de um nucleotídeo?",
            answers = new string[] { 
                "Açúcar, base, fosfato", 
                "Açúcar, base, aminoácido", 
                "Base, aminoácido, fosfato", 
                "Açúcar, lipídeo, base" 
            },
            correctIndex = 0,
            questionNumber = 3,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual açúcar está presente no RNA?",
            answers = new string[] { "Desoxirribose", "Ribose", "Glicose", "Frutose" },
            correctIndex = 1,
            questionNumber = 4,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual açúcar está presente no DNA?",
            answers = new string[] { "Desoxirribose", "Ribose", "Glicose", "Frutose" },
            correctIndex = 0,
            questionNumber = 5,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O que são nucleosídeos?",
            answers = new string[] {
                "Açúcar + base",
                "Açúcar + base + fosfato",
                "Base + fosfato",
                "Açúcar + aminoácido"
            },
            correctIndex = 0,
            questionNumber = 6,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Identifique a estrutura do nucleosídeo",
            answers = new string[] {
                "AnswerImages/NucleicAcidDB/nucleotideo_di_fosfato",
                "AnswerImages/NucleicAcidDB/nucleotideo_mono_fosfato",
                "AnswerImages/NucleicAcidDB/nucleosideo",
                "AnswerImages/NucleicAcidDB/nucleotideo_tri_fosfato"
            },
            correctIndex = 2,
            questionNumber = 7,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Quais bases são encontradas no RNA, mas não no DNA?",
            answers = new string[] { "Adenina, guanina", "Citosina, timina", "Uracila", "Timina, uracila" },
            correctIndex = 2,
            questionNumber = 8,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Quais bases são encontradas no DNA, mas não no RNA?",
            answers = new string[] { "Adenina, guanina", "Citosina, timina", "Uracila", "Timina" },
            correctIndex = 3,
            questionNumber = 9,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual a função principal dos grupamentos fosfato nos nucleotídeos?",
            answers = new string[] { "Dar caráter básico", "Dar caráter ácido", "Formar ligações peptídicas", "Armazenar energia" },
            correctIndex = 1,
            questionNumber = 10,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Que tipo de ligação une os nucleotídeos em uma cadeia?",
            answers = new string[] { "Ligação peptídica", "Ligação glicosídica", "Ligação éster", "Ligação fosfodiéster" },
            correctIndex = 3,
            questionNumber = 11,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual a orientação das cadeias de DNA em uma dupla hélice?",
            answers = new string[] { "Paralela", "Antiparalela", "Perpendicular", "Aleatória" },
            correctIndex = 1,
            questionNumber = 12,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O que são pares de bases de Watson-Crick?",
            answers = new string[] {
                "A-T e G-C",
                "A-G e T-C",
                "A-C e G-T",
                "Qualquer combinação de bases."
            },
            correctIndex = 0,
            questionNumber = 13,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual tipo de ligação mantém os pares de bases unidos no DNA?",
            answers = new string[] { "Ligação iônica", "Ligação covalente", "Pontes de hidrogênio", "Ligação peptídica" },
            correctIndex = 2,
            questionNumber = 14,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual a função principal do DNA?",
            answers = new string[] { "Transporte de moléculas", "Síntese de proteínas", "Armazenamento de informação genética", "Catálise de reações" },
            correctIndex = 2,
            questionNumber = 15,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual a função principal do RNA?",
            answers = new string[] { "Transporte de moléculas", "Síntese de proteínas", "Armazenamento de informação genética", "Expressão da informação genética" },
            correctIndex = 3,
            questionNumber = 16,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O que é desnaturação do DNA?",
            answers = new string[] {
                "Quebra da dupla hélice.",
                "Separação das fitas.",
                "Mudança na seqüência de bases.",
                "Todas as alternativas acima."
            },
            correctIndex = 1,
            questionNumber = 17,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O que é renaturação do DNA?",
            answers = new string[] {
                "Formação de novas fitas.",
                "Reassociação das fitas.",
                "Replicação do DNA.",
                "Transcrição do DNA."
            },
            correctIndex = 1,
            questionNumber = 18,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O que causa a desnaturação do DNA?",
            answers = new string[] {
                "Altas temperaturas",
                "Extremos de pH",
                "Ação de enzimas",
                "Todas as alternativas acima"
            },
            correctIndex = 3,
            questionNumber = 19,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Para que serve a medida de absorvância a 260nm?",
            answers = new string[] {
                "Medida da concentração de proteínas.",
                "Medida da concentração de ácidos nucléicos.",
                "Medida da temperatura de fusão do DNA.",
                "Medida da viscosidade de uma solução."
            },
            correctIndex = 1,
            questionNumber = 20,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O que é a regra de Chargaff?",
            answers = new string[] {
                "A = T e G = C",
                "A = G e T = C",
                "A = C e G = T",
                "Não há regra de Chargaff."
            },
            correctIndex = 0,
            questionNumber = 21,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Em que tipo de molécula a regra de Chargaff se aplica?",
            answers = new string[] {
                "DNA",
                "RNA",
                "Proteínas",
                "Carboidratos"
            },
            correctIndex = 0,
            questionNumber = 22,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O que é o 'fluxo da informação genética'?",
            answers = new string[] {
                "O movimento de íons através da membrana.",
                "A replicação do DNA.",
                "O processo de conversão da informação genética em proteínas.",
                "O transporte de proteínas para o exterior da célula."
            },
            correctIndex = 2,
            questionNumber = 23,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual tipo de RNA transporta aminoácidos para os ribossomos?",
            answers = new string[] { "tRNA", "rRNA", "mRNA", "snRNA" },
            correctIndex = 0,
            questionNumber = 24,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual tipo de RNA faz parte da estrutura dos ribossomos?",
            answers = new string[] { "tRNA", "rRNA", "mRNA", "snRNA" },
            correctIndex = 1,
            questionNumber = 25,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual a principal diferença química entre DNA e RNA?",
            answers = new string[] { "Açúcar", "Bases nitrogenadas", "Grupamento fosfato", "Sequência de bases" },
            correctIndex = 0,
            questionNumber = 26,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual a principal diferença na composição de bases entre DNA e RNA?",
            answers = new string[] { "Timina vs. Uracila", "Adenina vs. Guanina", "Citosina vs. Guanina", "Ribose vs. Desoxirribose" },
            correctIndex = 0,
            questionNumber = 27,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O que é um códon?",
            answers = new string[] {
                "Uma seqüência de três bases no tRNA.",
                "Uma seqüência de três bases no mRNA.",
                "Uma seqüência de três bases no rRNA.",
                "Uma seqüência de três bases no DNA."
            },
            correctIndex = 1,
            questionNumber = 28,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual a função do anticódon no tRNA?",
            answers = new string[] {
                "Ligar-se ao ribossomo.",
                "Ligar-se ao mRNA.",
                "Ligar-se a proteínas.",
                "Ligar-se ao DNA."
            },
            correctIndex = 1,
            questionNumber = 29,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual a função principal dos rRNAs?",
            answers = new string[] {
                "Transporte de aminoácidos.",
                "Síntese de proteínas.",
                "Fazem parte da estrutura dos ribossomos.",
                "Catalisam reações."
            },
            correctIndex = 2,
            questionNumber = 30,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Quais são os três componentes de um nucleotídeo?",
            answers = new string[] { "Açúcar, base, fosfato", "Açúcar, base, aminoácido", "Base, aminoácido, fosfato", "Açúcar, lipídeo, base" },
            correctIndex = 0,
            questionNumber = 31,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O que é um nucleosídeo?",
            answers = new string[] { "Açúcar + base + fosfato", "Açúcar + base", "Base + fosfato", "Açúcar + aminoácido" },
            correctIndex = 1,
            questionNumber = 32,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual o açúcar presente nos ribonucleotídeos?",
            answers = new string[] { "Desoxirribose", "Ribose", "Glicose", "Frutose" },
            correctIndex = 1,
            questionNumber = 33,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual o açúcar presente nos desoxirribonucleotídeos?",
            answers = new string[] { "Desoxirribose", "Ribose", "Glicose", "Frutose" },
            correctIndex = 0,
            questionNumber = 34,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Os ácidos nucleicos são polímeros formados por unidades chamadas:",
            answers = new string[] {
                "Aminoácidos",
                "Nucleotídeos",
                "Monossacarídeos",
                "Lipídios"
            },
            correctIndex = 1,
            questionNumber = 35,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Quais são os dois principais tipos de ácidos nucleicos encontrados nos seres vivos?",
            answers = new string[] {
                "DNA e RNA",
                "DNA e ATP",
                "RNA e lipídios",
                "DNA e proteínas"
            },
            correctIndex = 0,
            questionNumber = 36,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O açúcar presente no RNA é:",
            answers = new string[] {
                "Desoxirribose",
                "Glicose",
                "Ribose",
                "Maltose"
            },
            correctIndex = 2,
            questionNumber = 37,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
         new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "No DNA, a adenina sempre se emparelha com:",
            answers = new string[] {
                "Guanina",
                "Citosina",
                "Timina",
                "Uracila"
            },
            correctIndex = 2,
            questionNumber = 38,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
         new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "A estrutura de dupla hélice do DNA foi proposta por:",
            answers = new string[] {
                "Darwin e Lamarck",
                "Watson e Crick",
                "Pasteur e Koch",
                "Franklin e Mendel"
            },
            correctIndex = 1,
            questionNumber = 39,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O RNA que leva a informação genética do DNA até os ribossomos chama-se:",
            answers = new string[] {
                "RNA ribossômico (rRNA)",
                "RNA transportador (tRNA)",
                "RNA mensageiro (mRNA)",
                "RNA nuclear"
            },
            correctIndex = 2,
            questionNumber = 40,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual base nitrogenada é exclusiva do RNA e substitui a timina do DNA?",
            answers = new string[] {
                "Guanina",
                "Uracila",
                "Adenina",
                "Citosina"
            },
            correctIndex = 1,
            questionNumber = 41,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O processo de síntese de RNA a partir de DNA é chamado de:",
            answers = new string[] {
                "Tradução",
                "Transcrição",
                "Replicação",
                "Mutação"
            },
            correctIndex = 1,
            questionNumber = 42,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O nucleotídeo ATP é conhecido principalmente por:",
            answers = new string[] {
                "Formar a bicamada lipídica",
                "Ser uma fonte de energia celular",
                "Carregar oxigênio no sangue",
                "Transportar aminoácidos"
            },
            correctIndex = 1,
            questionNumber = 43,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O nucleotídeo ATP é conhecido principalmente por:",
            answers = new string[] {
                "Formar a bicamada lipídica",
                "Ser uma fonte de energia celular",
                "Carregar oxigênio no sangue",
                "Transportar aminoácidos"
            },
            correctIndex = 1,
            questionNumber = 44,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Durante a replicação do DNA, a enzima responsável por unir os nucleotídeos é a:",
            answers = new string[] {
                "DNA polimerase",
                "RNA polimerase",
                "Ligase",
                "Transcriptase reversa"
            },
            correctIndex = 0,
            questionNumber = 45,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Os ácidos nucleicos são macromoléculas formadas por unidades chamadas:",
            answers = new string[] {
                "Aminoácidos",
                "Nucleotídeos",
                "Monossacarídeos",
                "Lipídeos"
            },
            correctIndex = 1,
            questionNumber = 46,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Um nucleotídeo é composto por:",
            answers = new string[] {
                "Pentose + fosfato + base nitrogenada",
                "Hexose + lipídio + aminoácido",
                "Glicose + fosfato + proteína",
                "Glicerol + base nitrogenada + ácido graxo"
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
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "A base nitrogenada presente apenas no RNA é:",
            answers = new string[] {
                "Timina",
                "Citosina",
                "Uracila",
                "Adenina"
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
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "A base nitrogenada presente apenas no DNA é:",
            answers = new string[] {
                "Uracila",
                "Adenina",
                "Timina",
                "Guanina"
            },
            correctIndex = 2,
            questionNumber = 49,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "A molécula de DNA tem a forma característica conhecida como:",
            answers = new string[] {
                "Hélice simples",
                "Tripla hélice",
                "Dupla hélice",
                "Cadeia linear"
            },
            correctIndex = 2,
            questionNumber = 50,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "A pentose presente no DNA é:",
            answers = new string[] {
                "Ribose",
                "Desoxirribose",
                "Glicose",
                "Frutose"
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
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "No pareamento de bases do DNA, a adenina sempre se liga à:",
            answers = new string[] {
                "Guanina",
                "Citosina",
                "Uracila",
                "Timina"
            },
            correctIndex = 3,
            questionNumber = 52,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "A principal função do RNA mensageiro (RNAm) é:",
            answers = new string[] {
                "Formar a estrutura dos ribossomos",
                "Transportar aminoácidos",
                "Levar a informação genética do DNA até os ribossomos",
                "Catalisar reações químicas"
            },
            correctIndex = 2,
            questionNumber = 53,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O RNA transportador (RNAt) tem como função:",
            answers = new string[] {
                "Levar aminoácidos até os ribossomos durante a síntese proteica",
                "Carregar energia química",
                "Armazenar informação genética",
                "Catalisar reações metabólicas"
            },
            correctIndex = 0,
            questionNumber = 54,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual é a função principal do DNA nas células?",
            answers = new string[] {
                "Atuar como catalisador enzimático",
                "Fornecer energia imediata",
                "Armazenar e transmitir a informação genética",
                "Transportar oxigênio"
            },
            correctIndex = 2,
            questionNumber = 55,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Os ácidos nucleicos são formados por unidades chamadas:",
            answers = new string[] {
                "Aminoácidos",
                "Monossacarídeos",
                "Nucleotídeos",
                "Ácidos graxos"
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
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Cada nucleotídeo é composto por:",
            answers = new string[] {
                "Aminoácido, fosfato e água",
                "Açúcar, base nitrogenada e fosfato",
                "Glicerol, ácido graxo e base nitrogenada",
                "Açúcar, lipídio e proteína"
            },
            correctIndex = 1,
            questionNumber = 57,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O DNA possui como açúcar:",
            answers = new string[] {
                "Glicose",
                "Ribose",
                "Desoxirribose",
                "Galactose"
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
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O RNA possui como açúcar:",
            answers = new string[] {
                "Glicose",
                "Ribose",
                "Desoxirribose",
                "Maltose"
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
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Na estrutura de dupla hélice do DNA, as bases emparelham-se segundo a regra de Chargaff:",
            answers = new string[] {
                "A – G e C – T",
                "A – C e T – G",
                "A – T e C – G",
                "A – U e C – G"
            },
            correctIndex = 2,
            questionNumber = 60,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O RNA mensageiro (RNAm) tem como função principal:",
            answers = new string[] {
                "Transportar aminoácidos",
                "Atuar como catalisador enzimático",
                "Levar a informação do DNA até os ribossomos",
                "Formar a dupla hélice do DNA"
            },
            correctIndex = 2,
            questionNumber = 61,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O RNA transportador (RNAt) é responsável por:",
            answers = new string[] {
                "Levar aminoácidos até o ribossomo durante a síntese de proteínas",
                "Duplicar o DNA",
                "Formar a membrana celular",
                "Produzir energia na respiração"
            },
            correctIndex = 0,
            questionNumber = 62,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Quem descreveu o modelo da dupla hélice do DNA em 1953?",
            answers = new string[] {
                "Darwin e Mendel",
                "Watson e Crick",
                "Franklin e Chargaff",
                "Pauling e Wöhler"
            },
            correctIndex = 1,
            questionNumber = 63,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        }
    };

    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.nucleicAcids;
    public string GetDatabankName()  => "NucleicAcidsQuestionDatabase";
    public string GetDisplayName()   => "Ácidos Nucleicos";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;

}