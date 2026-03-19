using UnityEngine;
using System.Collections.Generic;
using QuestionSystem;

public class AminoacidQuestionDatabase : MonoBehaviour, IQuestionDatabase
{

    [Header("Development Settings")]
    [SerializeField] private bool databaseInDevelopment = false;

    private List<Question> questions = new List<Question>
    {
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "O que define um aminoácido?",
            answers = new string[] {
                "Uma molécula orgânica com um grupo amino e um grupo carboxila.",
                "Uma molécula inorgânica com um grupo amino e um grupo carboxila.",
                "Uma molécula orgânica com apenas um grupo amino.",
                "Uma molécula inorgânica com apenas um grupo carboxila."
            },
            correctIndex = 0,
            questionNumber = 1,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Qual o papel principal dos aminoácidos?",
            answers = new string[] {
                "Formar carboidratos.",
                "Formar lipídios.",
                "Formar proteínas.",
                "Formar ácidos nucléicos."
            },
            correctIndex = 2,
            questionNumber = 2,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Identifique o aminoácido cuja cadeia lateral apresenta característica polar não carregada.",
            answers = new string[] {
                "AnswerImages/AminoacidsDB/aminoacid_images/treonina",
                "AnswerImages/AminoacidsDB/aminoacid_images/glicina",
                "AnswerImages/AminoacidsDB/aminoacid_images/histidina",
                "AnswerImages/AminoacidsDB/aminoacid_images/alanina"
            },
            correctIndex = 0,
            questionNumber = 3,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "O que diferencia um aminoácido do outro?",
            answers = new string[] {
                "O grupo amino.",
                "O grupo carboxila.",
                "A sua cadeia lateral (R).",
                "O átomo de carbono alfa."
            },
            correctIndex = 2,
            questionNumber = 4,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Identifique o alfa-aminoácido abaixo",
            answers = new string[] {
                "AnswerImages/AminoacidsDB/aminoacid_images/alanina",
                "AnswerImages/AminoacidsDB/moleculas_organicas/3-amino-2-butanona",
                "AnswerImages/AminoacidsDB/moleculas_organicas/beta-alanina",
                "AnswerImages/AminoacidsDB/moleculas_organicas/2-amino-propanoato_de_metila"
            },
            correctIndex = 0,
            questionNumber = 5,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Aminoácidos com cadeias laterais alifáticas são:",
            answers = new string[] {
                "Polares.",
                "Apolares.",
                "Carregados positivamente.",
                "Carregados negativamente."
            },
            correctIndex = 1,
            questionNumber = 6,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Identifique o aminoácido que absorve o comprimento de onda de 280 nm.",
            answers = new string[] {
                "AnswerImages/AminoacidsDB/aminoacid_images/alanina",
                "AnswerImages/AminoacidsDB/aminoacid_images/treonina",
                "AnswerImages/AminoacidsDB/aminoacid_images/cisteina",
                "AnswerImages/AminoacidsDB/aminoacid_images/fenilalanina"
            },
            correctIndex = 3,
            questionNumber = 7,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/AminoacidsDB/moleculas_organicas/d-alanina",
                "AnswerImages/AminoacidsDB/aminoacid_images/treonina",
                "AnswerImages/AminoacidsDB/aminoacid_images/cisteina",
                "AnswerImages/AminoacidsDB/moleculas_organicas/d-alanina"
            },
            correctIndex = 3,
            questionNumber = 8,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer8",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "pH = 2,3",
                "pH = 6,0",
                "pH = 7,0",
                "pH = 9,7"
            },
            correctIndex = 1,
            questionNumber = 9,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer9",
            questionLevel = 2,
            questionInDevelopment = true
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/AminoacidsDB/aminoacid_images/isoleucina",
                "AnswerImages/AminoacidsDB/aminoacid_images/isoleucina_zw",
                "AnswerImages/AminoacidsDB/aminoacid_images/isoleucina_positiva",
                "AnswerImages/AminoacidsDB/aminoacid_images/isoleucina_negativa"
            },
            correctIndex = 2,
            questionNumber = 10,
            isImageAnswer = true,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer10",
            questionLevel = 2,
            questionInDevelopment = false
        },
         new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/AminoacidsDB/aminoacid_images/prolina",
                "AnswerImages/AminoacidsDB/aminoacid_images/prolina_zw",
                "AnswerImages/AminoacidsDB/aminoacid_images/prolina_positiva",
                "AnswerImages/AminoacidsDB/aminoacid_images/prolina_negativa"
            },
            correctIndex = 1,
            questionNumber = 11,
            isImageAnswer = true,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer11",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "pH = 5,5",
                "pH = 9,0",
                "pH = 10,7",
                "pH = 12,5"
            },
            correctIndex = 2,
            questionNumber = 12,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer12",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "pH = 3,0",
                "pH = 5,5",
                "pH = 3,9",
                "pH = 9,8"
            },
            correctIndex = 0,
            questionNumber = 13,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer13",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Os aminoácidos aspártico e glutâmico possuem:",
            answers = new string[] {
                "Um grupo carboxila no radical R.",
                "Um grupo amino no radical R.",
                "Um grupo sulfidrila no radical R.",
                "Um anel aromático no radical R."
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
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Identifique abaixo o aminoácido cuja cadeia lateral é considerada básica",
            answers = new string[] {
                "AnswerImages/AminoacidsDB/aminoacid_images/prolina",
                "AnswerImages/AminoacidsDB/aminoacid_images/isoleucina",
                "AnswerImages/AminoacidsDB/aminoacid_images/acido_aspartico",
                "AnswerImages/AminoacidsDB/aminoacid_images/arginina"
            },
            correctIndex = 3,
            questionNumber = 15,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Identifique abaixo o aminoácido cuja cadeia lateral é considerada ácida",
            answers = new string[] {
                "AnswerImages/AminoacidsDB/aminoacid_images/fenilalanina",
                "AnswerImages/AminoacidsDB/aminoacid_images/alanina",
                "AnswerImages/AminoacidsDB/aminoacid_images/acido_aspartico",
                "AnswerImages/AminoacidsDB/aminoacid_images/arginina"
            },
            correctIndex = 2,
            questionNumber = 16,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Identifique abaixo o aminoácido cuja cadeia lateral apresenta um grupo funcional álcool.",
            answers = new string[] {
                "AnswerImages/AminoacidsDB/aminoacid_images/tirosina",
                "AnswerImages/AminoacidsDB/aminoacid_images/prolina",
                "AnswerImages/AminoacidsDB/aminoacid_images/treonina",
                "AnswerImages/AminoacidsDB/aminoacid_images/leucina"
            },
            correctIndex = 2,
            questionNumber = 17,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Em pH ácido, o estado de protonação da maioria dos aminoácidos presentes na solução terá carga líquida:",
            answers = new string[] {
                "Negativa",
                "Neutra",
                "Positiva",
                "Variável"
            },
            correctIndex = 2,
            questionNumber = 18,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Identifique abaixo o aminoácido que absorve luz de comprimento de onda 280 nm.",
            answers = new string[] {
                "AnswerImages/AminoacidsDB/aminoacid_images/triptofano",
                "AnswerImages/AminoacidsDB/aminoacid_images/glutamina",
                "AnswerImages/AminoacidsDB/aminoacid_images/glicina",
                "AnswerImages/AminoacidsDB/aminoacid_images/alanina"
            },
            correctIndex = 0,
            questionNumber = 19,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Em pH básico, o estado de protonação da maioria dos aminoácidos presentes na solução terá carga líquida:",
            answers = new string[] {
                "Positiva",
                "Neutra",
                "Negativa",
                "Variável"
            },
            correctIndex = 2,
            questionNumber = 20,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "O que é um carbono quiral?",
            answers = new string[] {
                "Um carbono ligado a quatro átomos diferentes.",
                "Um carbono ligado a dois átomos iguais.",
                "Um carbono com dupla ligação.",
                "Um carbono com tripla ligação."
            },
            correctIndex = 0,
            questionNumber = 21,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Uma molécula com um carbono quiral é:",
            answers = new string[] {
                "Apolar",
                "Assimétrica",
                "Linear",
                "Simétrica"
            },
            correctIndex = 1,
            questionNumber = 22,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Isômeros que são imagens especulares um do outro, e NÃO são sobreponíveis:",
            answers = new string[] {
                "Enantiômeros",
                "Diasteroisômeros",
                "Isômeros constitucionais",
                "Isômeros conformacionais"
            },
            correctIndex = 0,
            questionNumber = 23,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "A designação D e L para aminoácidos se refere a:",
            answers = new string[] {
                "Sua composição química.",
                "Sua estrutura tridimensional.",
                "Sua solubilidade em água.",
                "Seu ponto isoelétrico."
            },
            correctIndex = 1,
            questionNumber = 24,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Quais aminoácidos são encontrados principalmente nas proteínas?",
            answers = new string[] {
                "D-aminoácidos",
                "L-aminoácidos",
                "Ambos em quantidades iguais",
                "Depende do organismo"
            },
            correctIndex = 1,
            questionNumber = 25,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Aminoácidos essenciais são aqueles que:",
            answers = new string[] {
                "Nosso corpo produz.",
                "Devem ser obtidos pela dieta.",
                "São encontrados em plantas.",
                "São encontrados em animais."
            },
            correctIndex = 1,
            questionNumber = 26,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Aminoácidos não-essenciais são aqueles que:",
            answers = new string[] {
                "Devem ser obtidos pela dieta.",
                "Nosso corpo produz.",
                "São encontrados apenas em animais.",
                "São encontrados apenas em plantas."
            },
            correctIndex = 1,
            questionNumber = 27,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Uma amida",
                "H<sup><size=150%> +</size></sup>",
                "Água",
                "OH<sup><size=150%> -</size></sup>"
            },
            correctIndex = 2,
            questionNumber = 28,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer28",
            questionLevel = 2,
            questionInDevelopment = false
        },
       new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Qual é o nome da ligação que ocorre entre os aminoácidos para forma proteínas",
            answers = new string[] {
                "Ponte de hidrogênio",
                "Ligação proteica",
                "Ligação peptídica",
                "Ligação eletrostática"
            },
            correctIndex = 2,
            questionNumber = 29,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Qual o nome do grupo funcional é criado pela condensação de dois aminoácidos para formar um peptídeo?",
            answers = new string[] {
                "Grupo funcional álcool",
                "Grupo funcional amina",
                "Grupo funcional ácido carboxílico",
                "Grupo funcional amida"
            },
            correctIndex = 3,
            questionNumber = 30,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "2 aminoácidos",
                "3 aminoácidos",
                "4 aminoácidos",
                "5 aminoácidos"
            },
            correctIndex = 2,
            questionNumber = 31,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer31",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Aminoácido",
                "Dipeptídeo",
                "Tripeptídeo",
                "Tetrapeptídeo"
            },
            correctIndex = 3,
            questionNumber = 32,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer32",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Aminoácidos com carga líquida positiva em pH 7 são:",
            answers = new string[] {
                "Ácidos",
                "Básicos",
                "Apolares",
                "Neutros"
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
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Aminoácidos com carga líquida negativa em pH 7 são:",
            answers = new string[] {
                "Básicos",
                "Ácidos",
                "Apolares",
                "Neutros"
            },
            correctIndex = 1,
            questionNumber = 34,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "O ponto isoelétrico (pI) de um aminoácido é:",
            answers = new string[] {
                "O pH em que ele é completamente protonado.",
                "O pH em que ele é completamente desprotonado.",
                "O pH em que sua carga líquida é zero.",
                "O pH em que sua solubilidade é máxima."
            },
            correctIndex = 2,
            questionNumber = 35,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Para um aminoácido com dois pKs, o pI é calculado como:",
            answers = new string[] {
                "A média dos dois pKs.",
                "A diferença entre os dois pKs.",
                "O maior dos dois pKs.",
                "O menor dos dois pKs."
            },
            correctIndex = 0,
            questionNumber = 36,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Em uma titulação de um aminoácido, os platôs na curva representam:",
            answers = new string[] {
                "Mudanças rápidas de pH.",
                "Mudanças lentas de pH.",
                "Dissociação de grupamentos ionizáveis.",
                "Adição de ácido ou base."
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
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "O que represente o pK de um grupamento ionizável em um aminoácido?",
            answers = new string[] {
                "O pH em que o grupamento está completamente protonado.",
                "O pH em que o grupamento está completamente desprotonado.",
                "O pH em que metade do grupamento está protonado e metade desprotonado.",
                "O pH em que o aminoácido tem carga líquida zero."
            },
            correctIndex = 2,
            questionNumber = 38,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "O que representa o pI de um aminoácido?:",
            answers = new string[] {
                "O pH em que ocorre a primeira dissociação.",
                "O pH em que ocorre a última dissociação.",
                "O pH em que a carga líquida do aminoácido é zero.",
                "O pH em que a concentração de H<sup><size=150%> +</size></sup> é máxima."
            },
            correctIndex = 2,
            questionNumber = 39,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Qual o nível estrutural de uma proteína que corresponde à sequência linear de aminoácidos?",
            answers = new string[] {
                "Estrutura secundária",
                "Estrutura terciária",
                "Estrutura quaternária",
                "Estrutura primária"
            },
            correctIndex = 3,
            questionNumber = 40,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "2 aminoácidos",
                "3 aminoácidos",
                "4 aminoácidos",
                "5 aminoácidos"
            },
            correctIndex = 3,
            questionNumber = 41,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer41",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "2 ligações peptídicas",
                "3 ligações peptídicas",
                "4 ligações peptídicas",
                "5 ligações peptídicas"
            },
            correctIndex = 1,
            questionNumber = 42,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer42",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Ponte de Hidrogênio",
                "Ponte Dissulfeto",
                "Interação hidrofóbica",
                "Interação eletrostática"
            },
            correctIndex = 1,
            questionNumber = 43,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer43",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Ponte de Hidrogênio",
                "Ponte Dissulfeto",
                "Interação hidrofóbica",
                "Interação eletrostática"
            },
            correctIndex = 0,
            questionNumber = 44,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer44",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Aminoácidos com a cadeia lateral R carregada negativamente são:",
            answers = new string[] {
                "Básicos",
                "Ácidos",
                "Neutros",
                "Apolares"
            },
            correctIndex = 1,
            questionNumber = 45,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Aminoácidos com a cadeia lateral R carregada positivamente são:",
            answers = new string[] {
                "Básicos",
                "Ácidos",
                "Neutros",
                "Apolares"
            },
            correctIndex = 0,
            questionNumber = 46,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Uma Ponte Dissulfeto",
                "Duas Pontes Dissulfeto",
                "Três Pontes Dissulfeto",
                "Não há Pontes Dissulfeto"
            },
            correctIndex = 2,
            questionNumber = 47,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer47",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Esturuta Primária",
                "Esturuta Secundária",
                "Esturuta Terciária",
                "Esturuta Quaternária"
            },
            correctIndex = 3,
            questionNumber = 48,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer48",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "O que é a estrutura terciária de uma proteína?",
            answers = new string[] {
                "A sua sequência linear de aminoácidos",
                "É a estrutura tridimensional da proteína",
                "São pequenas diferentes estruturas conservadas que dão forma a proteína.",
                "É a estrutura de três proteínas contectadas"
            },
            correctIndex = 1,
            questionNumber = 49,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Qual a importância das interações hidrofóbicas em proteínas?",
            answers = new string[] {
                "Não há interações hidrofóbicas em proteínas",
                "Servem para estabilizar as ligações peptídicas",
                "Permitem que as proteínas interajam com outras moléculas hidrofóbicas",
                "Estabilizam moléculas de água no interior das proteínas"
            },
            correctIndex = 2,
            questionNumber = 50,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Os aminoácidos são considerados os blocos de construção de qual macromolécula?",
            answers = new string[] {
                "Proteínas",
                "Carboidratos",
                "Lipídios",
                "Ácidos nucleicos"
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
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Os aminoácidos essenciais são aqueles que:",
            answers = new string[] {
                "Precisam ser obtidos pela dieta",
                "Não participam de síntese proteica",
                "São encontrados apenas em proteínas animais",
                "Podem ser sintetizados pelo corpo humano em qualquer condição"
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
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Qual aminoácido é conhecido por ser o mais simples, possuindo apenas um átomo de hidrogênio como cadeia lateral?",
            answers = new string[] {
                "Serina",
                "Alanina",
                "Prolina",
                "Glicina"
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
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Qual grupo funcional é característico de todos os aminoácidos?",
            answers = new string[] {
                "Amino e carboxila",
                "Aldeído e cetona",
                "Hidroxila e metila",
                "Fosfato e sulfato"
            },
            correctIndex = 0,
            questionNumber = 54,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Qual dos seguintes aminoácidos NÃO é essencial para o ser humano adulto?",
            answers = new string[] {
                "Valina",
                "Leucina",
                "Hidroxila e metila",
                "Glicina"
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
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Quantos aminoácidos proteicos canônicos existem no código genético padrão?",
            answers = new string[] {
                "30",
                "10",
                "64",
                "20"
            },
            correctIndex = 3,
            questionNumber = 56,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Qual aminoácido possui uma cadeia lateral com enxofre, sendo importante para a formação de pontes dissulfeto?",
            answers = new string[] {
                "Treonina",
                "Cisteína",
                "Fenilalanina",
                "Valina"
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
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "O grupo R de um aminoácido determina:",
            answers = new string[] {
                "O número de códons",
                "A ligação peptídica",
                "A quantidade de ATP produzido",
                "As propriedades químicas e estruturais do aminoácido"
            },
            correctIndex = 3,
            questionNumber = 58,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Os aminoácidos são considerados as unidades básicas de qual macromolécula?",
            answers = new string[] {
                "Lipídios",
                "Carboidratos",
                "Proteínas",
                "Ácidos nucleicos"
            },
            correctIndex = 2,
            questionNumber = 59,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Qual grupo funcional está presente em todos os aminoácidos?",
            answers = new string[] {
                "Hidroxila (OH) e fosfato (PO<sub><size=150%>4</size></sub><sup><size=150%>3-</size></sup>)",
                "Amino (NH<sub><size=150%>2</size></sub>) e carboxila (COOH)",
                "Sulfato (SO<sub><size=150%>4</size></sub><sup><size=150%>2-</size></sup>) e éster (COOR)",
                "Aldeído (CHO) e cetona (C=O)"
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
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "O que diferencia os aminoácidos entre si?",
            answers = new string[] {
                "O número de carbonos do grupo carboxila",
                "O tipo de ligação peptídica formada",
                "A cadeia lateral (radical R)",
                "A presença de nitrogênio no grupo amino"
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
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Quantos aminoácidos são considerados essenciais para humanos adultos?",
            answers = new string[] {
                "3",
                "9",
                "12",
                "20"
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
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Qual aminoácido contém enxofre em sua estrutura?",
            answers = new string[] {
                "Glicina",
                "Alanina",
                "Cisteína",
                "Lisina"
            },
            correctIndex = 2,
            questionNumber = 64,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "A ligação entre dois aminoácidos é chamada de:",
            answers = new string[] {
                "Ligação glicosídica",
                "Ligação peptídica",
                "Ligação fosfodiéster",
                "Ligação de hidrogênio"
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
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Qual aminoácido é o mais simples, com apenas um átomo de hidrogênio como cadeia lateral?",
            answers = new string[] {
                "Alanina",
                "Glicina",
                "Serina",
                "Prolina"
            },
            correctIndex = 1,
            questionNumber = 66,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Qual destes aminoácidos é aromático?",
            answers = new string[] {
                "Valina",
                "Fenilalanina",
                "Lisina",
                "Treonina"
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
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "A prolina é considerada um aminoácido especial porque:",
            answers = new string[] {
                "Não participa de ligações peptídicas",
                "Possui cadeia lateral aromática",
                "Sua cadeia lateral se liga ao próprio nitrogênio do grupo amino",
                "Não contém grupo carboxila"
            },
            correctIndex = 2,
            questionNumber = 68,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Em pH fisiológico (~7,4), um aminoácido geralmente está em qual forma?",
            answers = new string[] {
                "Totalmente protonado",
                "Totalmente desprotonado",
                "Zwitteriônica (com cargas positivas e negativas)",
                "Neutra, sem cargas"
            },
            correctIndex = 2,
            questionNumber = 69,
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
        return QuestionSet.aminoacids;
    }

    public string GetDatabankName()
    {
        return "AminoacidQuestionDatabase";
    }

    public bool IsDatabaseInDevelopment()
    {
        return databaseInDevelopment;
    }
}