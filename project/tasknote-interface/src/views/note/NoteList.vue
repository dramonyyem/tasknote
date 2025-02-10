<template>
    <div class="mx-auto" style="width: 600px;">
        <v-btn to="/note/create" block>Add Note</v-btn>
    </div>
    <div class="mx-auto" style="width: 100%;">
        <v-row
            class="mx-auto my-8"
            >
            <v-col
                v-for="item in responseData"
                :key="item.noteId"
                cols="6"
            >
                <v-card
                    @mouseover="hover = item.noteId"
                    @mouseleave="hover = null"
                    @click="messageData(item.noteId)"
                    :color="hover === item.noteId ? 'red-lighten-2' : '' "
                    elevation="16"
                    min-height="200px"
                    class="hover-box mx-auto my-8 border-radius-4"
                    >
                    <v-card-item>
                    <v-card-title>{{ item.title }}</v-card-title>

                </v-card-item>
                    <v-card-subtitle>Created At: {{ formatDateTime(item.createdAt) }}, Updated At: {{ formatDateTime(item.updatedAt) }}</v-card-subtitle>
                    <v-card-text>
                        {{ item.content }}
                    </v-card-text>

                    </v-card>
            </v-col>
        </v-row>
    </div>
    
</template>
<script lang="ts">
import axios from 'axios';
import { defineComponent, ref } from 'vue'
import { useRouter } from 'vue-router';

export default defineComponent({
    name: 'NoteList',
    setup () {
        const responseData = ref([]);
        const router = useRouter();
        const hover = ref<number | null>(null);
        const fetchData = async () => {
            try {
                const response = await axios.get("http://localhost:5001/api/notes",{ 
                    headers: {
                        'Authorization' : 'Bearer testing-token',
                    }
                });

                responseData.value = response.data;
            } catch (error) {
                
            }
        }

        const checkId = (id : number) => {
            alert(id);
        }

        const formatDateTime = (str : string) => {
            const date = new Date(str);
            const formatted = date.getFullYear() +
                '-' + String(date.getMonth() + 1).padStart(2, '0') +
                '-' + String(date.getDate()).padStart(2, '0') +
                ' ' + String(date.getHours()).padStart(2, '0') +
                ':' + String(date.getMinutes()).padStart(2, '0') +
                ':' + String(date.getSeconds()).padStart(2, '0'); // milliseconds with 3 digits
            return formatted;
        }

        const messageData = (id: number) => {
            router.push({ name: 'NoteDetail', params: { id } });

        }
        fetchData();
        return {
            fetchData,
            responseData,
            hover,
            formatDateTime,
            messageData
        }
    }
})
</script>

<style scoped>
.hover-box {
  padding: 20px;
  text-align: center;
  cursor: pointer;
  transition: background-color 0.3s ease;
}
</style>