package com.example.first;

// клас Споживач
class Consumer implements Runnable{
    Store store;

    Consumer (Store store){
        this.store = store;
    }

    public void run(){
        for (int i = 1; i < 6; i++){
            store.get();
        }
    }
}
