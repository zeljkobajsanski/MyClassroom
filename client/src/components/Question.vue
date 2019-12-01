<template>
    <form class="md-layout" @submit.prevent="save">
        <md-card>
            <md-card-content>
                <div class="md-layout-item">
                    <md-field>
                        <label>Question</label>
                        <md-input v-model="model.text"></md-input>
                    </md-field>
                    <md-field>
                        <div style="padding: 5px">
                            <mc-editor v-model="model.content"/>
                        </div>
                    </md-field>
                </div>
            </md-card-content>
            <md-card-actions>
                <md-button class="md-accent md-raised" @click="remove" :disabled="model.id == 0">Remove</md-button>
                <md-button class="md-primary md-raised" @click="save">Save</md-button>
            </md-card-actions>
        </md-card>
    </form>
</template>

<script lang="ts">
   import {mapState} from "vuex";

   export default {
       data() {
           return {

           }
       },
       props: ['model'],
       computed: {
           ...mapState({
               selectedChapter: state => state.selectedChapter
           })
       },
       methods: {
           async save() {

               this.id = await this.$store.dispatch('saveQuestion', {chapterId: this.selectedChapter, question: this.model})
           },
           async remove() {
               if (this.model.chapterId && this.model.id) {
                   await this.$store.dispatch('removeQuestion', this.model);
               }
           }
       }
   }
</script>

<style scoped>
    .md-card {
        width: 800px;
        height: 500px;
    }
</style>
