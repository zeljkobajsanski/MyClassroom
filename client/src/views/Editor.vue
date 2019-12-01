<template>
    <md-content class="md-layout">
        <div class="md-layout-item md-size-30">
            <md-card>
                <md-card-content>

                    <md-list>
                        <md-list-item v-for="theme in themes" :key="theme.id" @click="loadTheme(theme.id)">
                            <span class="md-list-item-link">{{theme.title}}</span>
                        </md-list-item>
                    </md-list>
                    <md-button class="md-fab md-fab-bottom-right" @click="openDialog">
                        <md-icon>add</md-icon>
                    </md-button>
                </md-card-content>
            </md-card>
        </div>

        <div class="md-layout-item md-alignment-left">
            <md-button class="md-primary md-raised" style="margin-left: 15px" @click="generateTest">Test</md-button>
            <Question v-for="q in questions" :key="q.id" :model="q" :themeId="currentTheme" />
        </div>

        <md-dialog-prompt :md-active.sync="showDialog"
                          md-title="Add new chapter"
                          md-confirm-text="Add"
                          v-model="newTheme"
                          @md-confirm="addNewTheme">
        </md-dialog-prompt>
    </md-content>
</template>

<script>
    import Question from "../components/Question.vue"
    import axios from 'axios';
    export default {
        components: {Question},
        data() {
            return {
                currentTheme: 0,
                themes: [],
                questions: [],
                showDialog: false,
                newTheme: ''
            }
        },
        watch: {
          '$route'() {
             this.created();
          }
        },
        async created() {
            await this.loadChapters();
        },
        methods: {
            openDialog() {
                this.showDialog = true;
            },
            async loadChapters() {
                const id = +this.$route.params.id;
                if (id) {
                    const { data } = await axios.get(`/api/books/${id}/themes`);
                    this.themes = data;
                    if (this.themes.length === 0) {
                        this.openDialog();
                    }
                }
            },
            async loadTheme(id) {
                const { data } = await axios.get(`/api/themes/${id}/questions`);
                this.questions = data;
                this.currentTheme = id;
            },
            async addNewTheme() {
                if (this.newTheme) {
                    const { data } = await axios.post(`/api/books/${this.$route.params.id}/themes`, {title: this.newTheme});
                    this.themes = data.themes;
                    this.currentTheme = data.id;
                }
            },
            generateTest() {
                this.$router.push('/test');
            }
        }
    }
</script>

<style scoped>

</style>
