import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios';

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        chapters: [],
        questions: [],
        selectedChapter: 0,
    },
    mutations: {
        setChapters(state, chapters) {
            state.chapters = [...chapters];
        },
        setQuestions(state, questions) {
            state.questions = questions;
        },
        setSelectedChapter(state, chapter) {
            state.selectedChapter = chapter;
        }
    },
    actions: {
        async loadChapters({ commit }, bookId) {
            const { data } = await axios.get(`/api/books/${bookId}/chapters`);
            commit('setChapters', data);
        },
        async addChapter({ commit }, payload) {
            const { data } = await axios.post(`/api/books/${payload.bookId}/chapters`, payload.chapter);
            commit('setChapters', data.chapters);
            return data.id;
        },
        async saveQuestion({ commit }, payload) {
            const { data } = await axios.post(`/api/chapters/${payload.chapterId}/questions`, payload.question);
            commit('setQuestions', data.questions);
            return data.id;
        },
        async removeQuestion({ commit }, question) {
            const { data } = await axios.delete(`/api/chapters/${question.chapterId}/questions/${question.id}`);
            commit('setQuestions', data);
        },
        async getQuestions({ commit }, chapterId) {
            const { data } = await axios.get(`/api/chapters/${chapterId}/questions`);
            commit('setQuestions', data);
            commit('setSelectedChapter', chapterId);
        },
    }
})
