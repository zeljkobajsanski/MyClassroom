<template>
    <div>
        <div v-for="t in test" :key="t.theme">
            <h3>{{t.theme}}</h3>
            <ol>
                <li  v-for="q in t.questions" :key="q.text">
                    <p>{{q.text}}</p>
                    <div v-html="q.content"></div>
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
            const { data } = await axios.post('/api/test');
            this.test = data;
        }
    }
</script>

<style scoped>

</style>
