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
                    <md-button class="md-fab md-primary md-fab-bottom-right" @click="openDialog">
                        <md-icon>add</md-icon>
                    </md-button>
                </md-card-content>
            </md-card>
        </div>

        <div class="md-layout-item md-alignment-left">
            <md-button class="md-primary md-raised" style="margin-left: 15px" @click="openTestGenerateDialog">Test</md-button>
            <Question v-for="q in questions" :key="q.id" :model="q" :chapterId="currentTheme" />
        </div>

        <md-dialog-prompt :md-active.sync="showDialog"
                          md-title="Add new chapter"
                          md-confirm-text="Add"
                          v-model="newTheme"
                          @md-confirm="addNewTheme">
        </md-dialog-prompt>

        <md-dialog :md-active.sync="showTestDialog">
            <md-dialog-title>Generate test</md-dialog-title>
            <md-dialog-content>
                <div>
                    <div v-for="chapter in themes" :key="chapter.id">
                        <md-checkbox v-model="selectedChapters" :value="chapter.id">
                            {{chapter.title}}
                        </md-checkbox>
                    </div>
                    <md-field>
                        <label>Percentage of questions</label>
                        <md-input type="number" v-model="percentage"></md-input>
                    </md-field>

                </div>
            </md-dialog-content>
            <md-dialog-actions>
                <md-button class="md-primary" @click="showTestDialog = false">Close</md-button>
                <md-button class="md-primary" @click="generateTest">Save</md-button>
            </md-dialog-actions>
        </md-dialog>
    </md-content>
</template>

<script>
    import Question from "../components/Question.vue"
    import {mapState} from "vuex";
    import axios from 'axios';

    export default {
        components: {Question},
        data() {
            return {
                currentTheme: 0,
                showDialog: false,
                newTheme: '',
                selectedChapters: [],
                showTestDialog: false,
                percentage: 30
            }
        },
        computed: mapState({
            themes: state => state.chapters,
            questions: state => state.questions
        }),
        watch: {
          '$route'() {
             this.loadChapters();
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
                    await this.$store.dispatch('loadChapters', id);

                    if (this.themes.length === 0) {
                        this.openDialog();
                    }
                }
            },
            async loadTheme(id) {
                await this.$store.dispatch('getQuestions', id);
                this.currentTheme = id;
            },
            async addNewTheme() {
                if (this.newTheme) {
                    const id = await this.$store.dispatch('addChapter', {bookId: +this.$route.params.id, chapter: {title: this.newTheme}});
                    this.currentTheme = id;
                }
            },
            openTestGenerateDialog() {
                this.showTestDialog = true;

            },
            async generateTest() {
                await axios.post('/api/test', {selectedChapters: this.selectedChapters, percentage: this.percentage});
                this.showTestDialog = false;
                await this.$router.push('/test');
            }
        }
    }
</script>

<style scoped>

</style>
