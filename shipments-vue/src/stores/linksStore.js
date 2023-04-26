import { defineStore } from "pinia";

export const useLinksStore = defineStore('linksStore',{
    state: () =>({
        url: 'https://localhost:44331/api/'
    })
})