"use strict";

let mouseMovements = [];

document.addEventListener("mousemove", (event) => {
    const entry = {
        x: event.clientX,
        y: event.clientY,
        t: Date.now()
    };
    console.log("Движение мыши:", entry); // Логируем события
    mouseMovements.push(entry);
});

function sendMouseData() {
    if (mouseMovements.length === 0) {
        console.warn("Нет данных для отправки");
        return;
    }

    console.log("Отправляем данные:", JSON.stringify(mouseMovements)); // Проверяем перед отправкой

    fetch("http://localhost:5240/mouse-tracking", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(mouseMovements)
    })
    .then(response => response.text())
    .then(data => {
        alert(data.message); // Выводим сообщение
        mouseMovements = []; // Очистка массива после отправки
    })
    .catch(error => console.error("Ошибка:", error));
}