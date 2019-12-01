<template>
    <div>
        <div v-for="t in test" :key="t.Theme">
            <h3>{{t.theme}}</h3>
            <ol>
                <li  v-for="q in t.Questions" :key="q.Text">
                    <p>{{q.Text}}</p>
                    <div v-html="q.Content"></div>
                </li>
            </ol>
        </div>
    </div>
</template>

<script>
    import axios from 'axios'

    export default {
        data() {
            return {
                test: []
            }
        },
        async created() {
            const { data } = await axios.get('/api/test');
            this.test = data;
        }
    }
</script>

<style scoped>

</style>
