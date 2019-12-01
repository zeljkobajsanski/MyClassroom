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
                <md-button class="md-primary md-dense" @click="save">Save</md-button>
            </md-card-actions>
        </md-card>
    </form>
</template>

<script lang="ts">
    import axios from 'axios'
   export default {
       data() {
           return {
               id: 0,
               text: '',
               content: ''
           }
       },
       props: ['model'],
       methods: {
           async save() {
               const { data } = await axios.post(`/api/themes/${this.$attrs.themeId}/questions`, this.model);
               this.id = data.id;
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
