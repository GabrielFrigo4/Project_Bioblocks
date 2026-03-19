const {onRequest} = require("firebase-functions/v2/https");
const admin = require("firebase-admin");
admin.initializeApp();

exports.resetWeeklyScores = onRequest(async (req, res) => {
  const secretKey = process.env.RESET_SECRET_KEY;

  if (!secretKey) {
    console.error("RESET_SECRET_KEY não configurada no ambiente.");
    res.status(500).send("Configuração interna inválida.");
    return;
  }

  if (req.query.key !== secretKey) {
    console.warn("Tentativa de acesso com chave inválida.");
    res.status(403).send("Acesso não autorizado.");
    return;
  }

  try {
    const db = admin.firestore();
    const usersRef = db.collection("Users");
    const snapshot = await usersRef.get();
    const batch = db.batch();
    let count = 0;

    snapshot.forEach((doc) => {
      batch.update(doc.ref, {"WeekScore": 0});
      count++;
    });

    await batch.commit();

    const msg = `Scores semanais resetados para ${count} usuários.`;
    console.log(msg);
    res.status(200).send(msg);
  } catch (error) {
    console.error("Erro ao resetar scores semanais:", error);
    res.status(500).send(`Erro: ${error.message}`);
  }
});