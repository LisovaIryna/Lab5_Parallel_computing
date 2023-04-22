package com.example.second;

import java.util.concurrent.Semaphore;

// клас Філософ
class Philosopher extends Thread {
    Semaphore sem; // семафор, який обмежує кількість філософів
    int num = 0; // кількість прийомів їжі
    int id; // умовний номер філософа

    // в якості параметрів конструктора передаються ідентифікатор філософа і семафор
    Philosopher(Semaphore sem, int id) {
        this.sem = sem;
        this.id = id;
    }

    public void run() {
        try {
            while (num < 3) { // поки кількість прийомів іжі не досягне 3
                // Запит у семафора дозволу на виконання
                sem.acquire();
                System.out.println("Філософ " + id + " сідає за стіл");
                // філософ їсть
                sleep(500);
                num++;

                System.out.println("Філософ " + id + " встає з-за столу");
                sem.release();

                // філософ гуляє
                sleep(500);
            }
        }
        catch (InterruptedException e) {
            System.out.println("У філософа " + id + " проблеми із здоров'ям");
        }
    }
}
