using UnityEngine;
using System.Collections.Generic;
using QuestionSystem;

public class MembranesQuestionDatabase : MonoBehaviour, IQuestionDatabase
{

    [Header("Development Settings")]
    [SerializeField] private bool databaseInDevelopment = false;
    
    private List<Question> questions = new List<Question>
    {
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual o principal componente de uma membrana biológica?",
            answers = new string[] {
                "Carboidratos", 
                "Lipídeos", 
                "Proteínas", 
                "Ácidos Nucleicos"},
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
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Quais os três principais tipos de lipídeos encontrados em membranas biológicas?",
            answers = new string[] {
                "Triacilgliceróis, fosfolipídeos, esfingolipídeos", 
                "Glicerofosfolipídeos, esfingolipídeos, esteroides", 
                "Ácidos graxos, colesterol, triglicerídeos", 
                "Ceras, esteroides, glicerofosfolipídeos"},
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
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O que significa anfipático?",
            answers = new string[] { 
                "Apresentar regiões polares e apolares", 
                "Apenas polar", 
                "Apenas apolar", 
                "Solúvel apenas em água"},
            correctIndex = 0,
            questionNumber = 3,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
       },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual a principal função da membrana plasmática?",
            answers = new string[] { 
                "Produção de energia", 
                "Síntese de proteínas", 
                "Manutenção do ambiente celular", 
                "Remoção de resíduos"},
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
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Que tipo de ligação une os ácidos graxos ao glicerol nos glicerofosfolipídeos?",
            answers = new string[] { 
                "Ligação peptídica", 
                "Ligação glicosídica", 
                "Ligação éster", 
                "Ligação fosfodiéster"},
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
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual destes NÃO é um componente dos glicerofosfolipídeos?",
            answers = new string[] { 
                "Ácidos graxos", 
                "Glicerol", 
                "Esfingosina", 
                "Fosfato"},
            correctIndex = 2,
            questionNumber = 6,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
       },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual a função do grupo de cabeça polar em um fosfolipídeo?",
            answers = new string[] { 
                "Interage com a água", 
                "Forma a cauda hidrofóbica", 
                "Fornece rigidez estrutural", 
                "Armazena energia"},
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
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Que tipo de ligação une o grupo fosfato ao glicerol em um glicerofosfolipídeo?",
            answers = new string[] { 
                "Ligação peptídica", 
                "Ligação glicosídica",
                 "Ligação éster", 
                 "Ligação fosfodiéster" },
            correctIndex = 3,
            questionNumber = 8,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual o nome do glicerofosfolipídeo mais comum?",
            answers = new string[] { 
                "Esfingomielina", 
                "Fosfatidilcolina", 
                "Cardiolipina", 
                "Cerebrosídeo" },
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
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual a função da cardiolipina?",
            answers = new string[] { 
                "Isolamento", 
                "Armazenamento de energia", 
                "Encontrada principalmente em membranas mitocondriais", 
                "Sinalização celular" },
            correctIndex = 2,
            questionNumber = 10,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual destas substâncias não está presente em membranas celulares?",
            answers = new string[] {
                "AnswerImages/LipidDB/glicolipideo",
                "AnswerImages/LipidDB/porfirina",
                "AnswerImages/LipidDB/fosfatidilcolina",
                "AnswerImages/LipidDB/colesterol"
            },
            correctIndex = 1,
            questionNumber = 11,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual a unidade estrutural básica dos esfingolipídeos?",
            answers = new string[] { 
                "Glicerol", 
                "Esfingosina", 
                "Ácido graxo", 
                "Fosfato" },
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
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Do que a ceramida é composta?",
            answers = new string[] { 
                "Esfingosina e um ácido graxo", 
                "Glicerol e ácidos graxos", 
                "Esfingosina e fosfato", 
                "Glicerol e esfingosina" },
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
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O que diz o modelo do mozaico fluido da formação de membranas celulares?",
            answers = new string[] {
                "Membranas apresentam-se como um grande mozaico de lipídeos.",
                "A estrutura de uma membrana celular não é estática, e os lipídeos podem movimentar-se através dela.",
                "O colesterol move-se livremente na membrana.",
                "Membranas são basicamente lipídeos, sem nenhum outro tipo de molécula em sua composição"
            },
            correctIndex = 1,
            questionNumber = 14,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O que são gangliosídeos?",
            answers = new string[] { 
                "Esfingolipídeos simples", 
                "Esfingolipídeos complexos com oligossacarídeos e ácido siálico", 
                "Glicerofosfolipídeos", 
                "Esteroides" },
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
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual a estrutura básica dos esteroides?",
            answers = new string[] { 
                "Três anéis de seis carbonos e um anel de cinco carbonos", 
                "Uma longa cadeia de hidrocarbonetos", 
                "Uma estrutura de glicerol", 
                "Um grupo fosfato" },
            correctIndex = 0,
            questionNumber = 16,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual o esteroide mais abundante em células animais?",
            answers = new string[] { 
                "Estrogênio", 
                "Testosterona", 
                "Colesterol", 
                "Cortisol" },
            correctIndex = 2,
            questionNumber = 17,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual a principal função do colesterol nas membranas?",
            answers = new string[] { 
                "Armazenamento de energia", 
                "Transdução de sinal", 
                "Modulação da fluidez da membrana", 
                "Atividade enzimática" },
            correctIndex = 2,
            questionNumber = 18,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O que é a monocamada de Langmuir?",
            answers = new string[] { 
                "Uma bicamada lipídica", 
                "Uma única camada de lipídeos na superfície da água", 
                "Uma micela lipídica", 
                "Um tipo de proteína" },
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
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O que Gorter e Grendel concluíram sobre o arranjo dos lipídeos na membrana celular?",
            answers = new string[] { 
                "Os lipídeos formam uma monocamada", 
                "Os lipídeos formam uma bicamada", 
                "Os lipídeos formam micelas", 
                "Os lipídeos estão distribuídos aleatoriamente" },
            correctIndex = 1,
            questionNumber = 20,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Que técnica Frye e Edidin usaram para estudar a fluidez da membrana?",
            answers = new string[] { 
                "Microscopia eletrônica", 
                "Difração de raios-X", 
                "Fusão celular com marcadores fluorescentes", 
                "Ressonância magnética nuclear" },
            correctIndex = 2,
            questionNumber = 21,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O que é o modelo do mosaico fluido?",
            answers = new string[] { 
                "Um modelo da estrutura da membrana celular", 
                "Um modelo de enovelamento de proteínas", 
                "Um modelo de replicação do DNA", 
                "Um modelo de metabolismo de carboidratos" },
            correctIndex = 0,
            questionNumber = 22,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual o principal tipo de movimento dos fosfolipídeos dentro de uma membrana?",
            answers = new string[] { 
                "Flip-flop", 
                "Difusão lateral", 
                "Rotação", 
                "Difusão transversal" },
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
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Utilizados na determinação do tipo sanguíneo",
                "Utilizados como biomedicamentos para várias doenças",
                "São marcadores tumorais",
                "Agem como hormônios no sistema nervoso central"
            },
            correctIndex = 0,
            questionNumber = 24,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/MembraneDB/membraneDB_ImageQuestionContainer24",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Como os ácidos graxos saturados afetam a fluidez da membrana?",
            answers = new string[] { 
                "Aumentam a fluidez", 
                "Diminuem a fluidez", 
                "Não têm efeito", 
                "Aumentam a permeabilidade" },
            correctIndex = 1,
            questionNumber = 25,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Como os ácidos graxos insaturados afetam a fluidez da membrana?",
            answers = new string[] { 
                "Aumentam a fluidez", 
                "Diminuem a fluidez", 
                "Não têm efeito", 
                "Aumentam a permeabilidade" },
            correctIndex = 0,
            questionNumber = 26,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "A bainha de mielina é composta principalmente por:",
            answers = new string[] {
                "Proteínas",
                "Glicerofosfolipídeos",
                "Esfingolipídeos",
                "Ácidos nucléicos"
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
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual o papel das proteínas na membrana celular?",
            answers = new string[] { 
                "Suporte estrutural", 
                "Transporte", 
                "Receptores", 
                "Todas as alternativas acima" },
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
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O que é uma ligação fosfodiéster?",
            answers = new string[] { 
                "Uma ligação entre dois açúcares", 
                "Uma ligação entre um fosfato e dois álcoois", 
                "Uma ligação entre dois ácidos graxos", 
                "Uma ligação entre um fosfato e o glicerol" },
            correctIndex = 1,
            questionNumber = 29,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual a diferença entre uma micela e uma bicamada?",
            answers = new string[] { 
                "As micelas são esféricas, as bicamadas são planares", 
                "As micelas são polares, as bicamadas são apolares", 
                "As micelas são encontradas no citoplasma, as bicamadas são encontradas na membrana", 
                "As micelas são pequenas, as bicamadas são grandes" },
            correctIndex = 0,
            questionNumber = 30,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual a função da bicamada fosfolipídica?",
            answers = new string[] { 
                "Formar uma barreira seletivamente permeável", 
                "Fornecer energia para a célula", 
                "Armazenar informação genética", 
                "Sintetizar proteínas" },
            correctIndex = 0,
            questionNumber = 31,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual a importância da fluidez da membrana?",
            answers = new string[] { 
                "Permite o movimento e a função das proteínas da membrana", 
                "Permite a sinalização celular", 
                "Mantém a integridade da membrana", 
                "Todas as alternativas acima" },
            correctIndex = 3,
            questionNumber = 32,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Quais fatores afetam a fluidez da membrana?",
            answers = new string[] { 
                "Temperatura, composição lipídica, teor de colesterol", 
                "pH, pressão, atividade enzimática", 
                "Intensidade de luz, concentração de oxigênio, salinidade", 
                "Todas as alternativas acima" },
            correctIndex = 0,
            questionNumber = 33,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual o papel dos glicolipídeos na membrana celular?",
            answers = new string[] { 
                "Reconhecimento celular", 
                "Armazenamento de energia", 
                "Suporte estrutural", 
                "Atividade enzimática" },
            correctIndex = 0,
            questionNumber = 34,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual a função do movimento flip-flop dos fosfolipídeos?",
            answers = new string[] { 
                "Manter a assimetria da membrana", 
                "Facilitar o transporte de proteínas", 
                "Regular a fluidez da membrana", 
                "Armazenar energia" },
            correctIndex = 0,
            questionNumber = 35,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual o significado do experimento conduzido por Frye e Edidin?",
            answers = new string[] { 
                "Demonstrou a fluidez da membrana", 
                "Confirmou a estrutura da bicamada lipídica", 
                "Identificou as proteínas da membrana", 
                "Mediu a espessura da membrana" },
            correctIndex = 0,
            questionNumber = 36,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Formam micelas perfeitas em solução aquosa.",
                "São importantes para a formação de membranas de camada simples",
                "Fazem parte da composição de muitas membranas biológicas",
                "Ao reagirem com bases formam ótimos biocombustíveis"
            },
            correctIndex = 2,
            questionNumber = 37,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/MembraneDB/membraneDB_ImageQuestionContainer37",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Glicerofosfoliplideos",
                "Esfingolipídeos",
                "Esteróis",
                "Ceramidas"
            },
            correctIndex = 0,
            questionNumber = 38,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/MembraneDB/membraneDB_ImageQuestionContainer38",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Indique abaixo o lipídeo pertencente a família dos esfingolipídeos",
            answers = new string[] {
                "AnswerImages/LipidDB/acido_graxo_saturado",
                "AnswerImages/LipidDB/glicolipideo",
                "AnswerImages/LipidDB/fosfatidilcolina",
                "AnswerImages/LipidDB/colesterol"
            },
            correctIndex = 1,
            questionNumber = 39,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual a importância da composição lipídica na manutenção da fluidez da membrana?",
            answers = new string[] { 
                "Ácidos graxos saturados diminuem a fluidez, ácidos graxos insaturados aumentam a fluidez", 
                "Ácidos graxos saturados aumentam a fluidez, ácidos graxos insaturados diminuem a fluidez", 
                "A composição lipídica não tem efeito na fluidez", 
                "O comprimento das caudas dos ácidos graxos determina a fluidez" },
            correctIndex = 0,
            questionNumber = 40,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual o significado do modelo do mosaico fluido da membrana?",
            answers = new string[] { 
                "Explica a fluidez da membrana e a mobilidade das proteínas", 
                "Descreve a interação entre lipídeos e proteínas", 
                "Fornece uma estrutura para entender a função da membrana", 
                "Todas as alternativas acima" },
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
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual o papel dos componentes de carboidratos na membrana celular?",
            answers = new string[] { 
                "Sinalização e reconhecimento celular", 
                "Suporte estrutural", 
                "Respostas imunológicas", 
                "Todas as alternativas acima" },
            correctIndex = 3,
            questionNumber = 42,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual a diferença entre proteínas integrais e periféricas da membrana?",
            answers = new string[] { 
                "As proteínas integrais atravessam a membrana, as proteínas periféricas estão frouxamente associadas", 
                "As proteínas integrais são hidrofílicas, as proteínas periféricas são hidrofóbicas", 
                "As proteínas integrais são encontradas no citoplasma, as proteínas periféricas são encontradas na superfície", 
                "As proteínas integrais são enzimas, as proteínas periféricas são estruturais" },
            correctIndex = 0,
            questionNumber = 43,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "A membrana plasmática é composta principalmente por:",
            answers = new string[] { 
                "Proteínas e ácidos nucleicos", 
                "Lipídios e proteínas", 
                "Carboidratos e aminoácidos", 
                "Água e sais minerais" },
            correctIndex = 1,
            questionNumber = 44,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O modelo aceito para descrever a estrutura da membrana plasmática é chamado:",
            answers = new string[] { 
                "Modelo mosaico fluido", 
                "Modelo chave-fechadura", 
                "Modelo helicoidal", 
                "Modelo tripla hélice" },
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
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual é o lipídeo mais abundante nas membranas celulares?",
            answers = new string[] { 
                "Triglicerídeos", 
                "Fosfolipídios", 
                "Esteroides", 
                "Cerídeos" },
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
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "A bicamada lipídica é formada por fosfolipídios que apresentam regiões:",
            answers = new string[] { 
                "Totalmente polares", 
                "Totalmente apolares", 
                "Polares e apolares (anfipáticas)", 
                "Apenas hidrofílicas" },
            correctIndex = 2,
            questionNumber = 47,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "A bicamada lipídica é formada por fosfolipídios que apresentam regiões:",
            answers = new string[] { 
                "Totalmente polares", 
                "Totalmente apolares", 
                "Polares e apolares (anfipáticas)", 
                "Apenas hidrofílicas" },
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
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O colesterol, presente nas membranas, tem como função principal:",
            answers = new string[] { 
                "Armazenar energia", 
                "Regular a fluidez da membrana", 
                "Transportar oxigênio", 
                "Produzir ATP" },
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
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "As proteínas que atravessam toda a bicamada lipídica são chamadas de:",
            answers = new string[] { 
                "Proteínas periféricas", 
                "Proteínas integrais", 
                "Enzimas extracelulares", 
                "Proteínas nucleares" },
            correctIndex = 1,
            questionNumber = 50,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Os carboidratos presentes na membrana estão associados principalmente a:",
            answers = new string[] { 
                "Reconhecimento celular", 
                "Produção de energia imediata", 
                "Transporte ativo", 
                "Síntese proteica" },
            correctIndex = 0,
            questionNumber = 51,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O transporte de moléculas contra o gradiente de concentração, com gasto de energia, é chamado:",
            answers = new string[] { 
                "Difusão simples", 
                "Difusão facilitada", 
                "Transporte ativo", 
                "Osmose" },
            correctIndex = 2,
            questionNumber = 52,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "A passagem de água pela membrana sem gasto de energia recebe o nome de:",
            answers = new string[] { 
                "Osmose", 
                "Transporte ativo", 
                "Endocitose", 
                "Exocitose" },
            correctIndex = 0,
            questionNumber = 53,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual dos processos abaixo envolve a entrada de partículas grandes ou fluidos pela membrana?",
            answers = new string[] { 
                "Osmose", 
                "Difusão simples", 
                "Endocitose", 
                "Transporte passivo" },
            correctIndex = 2,
            questionNumber = 54,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "A função principal das proteínas de membrana é:",
            answers = new string[] { 
                "Atuar como enzimas, transportadores ou receptores", 
                "Fornecer energia para a célula", 
                "Servir como reserva de aminoácidos", 
                "Produzir ATP" },
            correctIndex = 0,
            questionNumber = 55,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O colesterol na membrana plasmática atua principalmente:",
            answers = new string[] { 
                "Fornecendo energia à célula", 
                "Estabilizando a fluidez da membrana", 
                "Participando da respiração celular", 
                "Transportando oxigênio" },
            correctIndex = 1,
            questionNumber = 56,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O transporte passivo é caracterizado por:",
            answers = new string[] { 
                "Consumo de ATP", 
                "Movimento contra o gradiente de concentração", 
                "Movimento a favor do gradiente de concentração sem gasto de energia", 
                "Exclusivamente realizado por proteínas" },
            correctIndex = 2,
            questionNumber = 57,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual das opções abaixo é um exemplo de transporte ativo?",
            answers = new string[] { 
                "Difusão simples", 
                "Difusão facilitada", 
                "Osmose", 
                "Bomba de sódio e potássio" },
            correctIndex = 3,
            questionNumber = 58,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "A entrada de água através da membrana por diferença de concentração é chamada de:",
            answers = new string[] { 
                "Pinocitose", 
                "Osmose", 
                "Difusão simples", 
                "Transporte ativo" },
            correctIndex = 1,
            questionNumber = 59,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "As glicoproteínas e glicolipídeos da membrana têm papel fundamental em:",
            answers = new string[] { 
                "Armazenar energia", 
                "Formação de ATP", 
                "Reconhecimento celular e comunicação", 
                "Produção de hormônios" },
            correctIndex = 2,
            questionNumber = 60,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O principal modelo que descreve a estrutura da membrana plasmática é chamado de:",
            answers = new string[] { 
                "Modelo do mosaico fluido", 
                "Modelo da dupla hélice", 
                "Modelo chave-fechadura", 
                "Modelo do tapete contínuo" },
            correctIndex = 0,
            questionNumber = 61,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "As membranas biológicas são constituídas principalmente por:",
            answers = new string[] { 
                "Proteínas e ácidos nucleicos", 
                "Lipídeos e carboidratos", 
                "Lipídeos e proteínas", 
                "Carboidratos e aminoácidos" },
            correctIndex = 2,
            questionNumber = 62,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Os lipídeos mais abundantes nas membranas celulares são:",
            answers = new string[] { 
                "Glicerídeos", 
                "Fosfolipídeos", 
                "Esteroides", 
                "Carotenoides" },
            correctIndex = 1,
            questionNumber = 63,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual lipídeo ajuda a regular a fluidez da membrana plasmática em células animais?",
            answers = new string[] { 
                "Triglicerídeos", 
                "Colesterol", 
                "Carotenoides", 
                "Ácidos graxos livres" },
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
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "As proteínas que atravessam completamente a bicamada lipídica são chamadas de:",
            answers = new string[] { 
                "Proteínas periféricas", 
                "Proteínas integrais de membrana", 
                "Enzimas citoplasmáticas", 
                "Proteínas ribossômicas" },
            correctIndex = 1,
            questionNumber = 65,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Os carboidratos presentes na membrana estão ligados principalmente a:",
            answers = new string[] { 
                "Aminoácidos essenciais", 
                "DNA e RNA", 
                "Fosfolipídeos e proteínas", 
                "Colesterol e triglicerídeos" },
            correctIndex = 2,
            questionNumber = 66,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "A função mais importante da membrana plasmática é:",
            answers = new string[] { 
                "Produzir energia", 
                "Estocar material genético", 
                "Fosfolipídeos e proteínas", 
                "Regular a entrada e saída de substâncias" },
            correctIndex = 3,
            questionNumber = 67,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O transporte que ocorre a favor do gradiente de concentração, sem gasto de energia, é chamado de:",
            answers = new string[] { 
                "Transporte ativo", 
                "Osmose", 
                "Transporte passivo", 
                "Endocitose" },
            correctIndex = 2,
            questionNumber = 68,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "A difusão facilitada se diferencia da difusão simples porque:",
            answers = new string[] { 
                "Precisa de energia (ATP)", 
                "Utiliza proteínas transportadoras ou canais", 
                "Só ocorre em soluções hipertônicas", 
                "É exclusiva de bactérias" },
            correctIndex = 1,
            questionNumber = 69,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        },
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Quando a célula engloba partículas grandes por meio da membrana, esse processo é chamado de:",
            answers = new string[] { 
                "Exocitose", 
                "Pinocitose", 
                "Fagocitose", 
                "Difusão" },
            correctIndex = 2,
            questionNumber = 70,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false
        }
    };
    
    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.membranes;
    public string GetDatabankName()  => "MembranesQuestionDatabase";
    public string GetDisplayName()   => "Membranas Biológicas";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;

}