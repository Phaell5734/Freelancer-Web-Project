const express = require('express');
const bodyParser = require('body-parser');

const app = express();
app.use(bodyParser.json());

// Kullanıcıları saklamak için basit bir nesne (gerçek uygulamada bir veritabanı kullanılmalıdır)
const users = {
    1: { userId: 1, balance: 0 }
};

// www.youtube.com takip etme endpoint'i
app.post('/follow-youtube-channel', (req, res) => {
    const { userId, amount } = req.body;

    // kullanıcının bakiyesini güncelle
    users[userId].balance += amount;


    res.status(200).json({ message: 'www.youtube.com takip edildi. Bakiyenize 100 birim eklendi.' });
});

// server'ı dinle
const PORT = 3006;
app.listen(PORT, () => {
    console.log(Server çalışıyor: http://localhost:${PORT});
});