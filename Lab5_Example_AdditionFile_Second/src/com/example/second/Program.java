package com.example.second;

import java.util.concurrent.Semaphore;

public class Program {
    public static void main(String[] args){
        Semaphore sem = new Semaphore(2);
        for(int i = 1; i < 6; i++)
            new Philosopher(sem,i).start();
    }
}
