<template>
    <v-card
        class="mx-auto my-8"
        elevation="16"
        min-width="600"
        min-height="200"
        title="User"
        subtitle="Detail"

    >
        
        <v-card-item>
            <v-row cols="12">
                <v-col>
                    User ID :
                </v-col>
                <v-col class="text-left">
                    {{ responseData.userId }}
                </v-col>
            </v-row>
            <v-row>
                <v-col>
                    Name :
                </v-col>
                <v-col class="text-left">
                    {{ responseData.name }}
                </v-col>
            </v-row>
        </v-card-item>
    </v-card>
        <v-row 
            style="width: 1000px;"
            class="mx-auto my-8"
            >
            <v-col
                v-for="item in responseNoteData"
                :key="item.noteId"
                lg="6"
                
                sm="12"
            >
                <v-card
                    @mouseover="hover = item.noteId"
                    @mouseleave="hover = null"
                    :color="hover === item.noteId ? 'red-lighten-2' : '' "
                    elevation="16"
                    min-height="200px"
                    class="hover-box mx-auto my-8 border-radius-4"
                    >
                    <v-card-item>
                    <v-card-title>{{ item.title }}</v-card-title>

                </v-card-item>
                    <v-card-subtitle>Created At: {{ formatDateTime(item.createdAt) }}</v-card-subtitle>
                    <v-card-subtitle>Updated At: {{ formatDateTime(item.updatedAt) }}</v-card-subtitle>

                   
                    <v-card-text>
                        {{ item.content }}
                    </v-card-text>
                    <div style="width: 90%;" class="mx-auto">
                        <v-btn
                        @click="deleteNote(item.noteId)"
                        block>
                            remove
                        </v-btn>
                    </div>
                   
                    </v-card>
            </v-col>
        </v-row>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'
import axios from 'axios';
import { useRoute } from 'vue-router'
import { useRouter } from 'vue-router';


export default defineComponent({
    name: 'UserDetail',
    setup () {
        const hover = ref<number | null>(null);
        const route = useRoute();
        const router = useRouter();

        const responseData = ref([]);
        const responseNoteData = ref([]);
        let userId = route.params.id;
        const getUser = async () => {
            try {
                const response = await axios.get(`http://localhost:5001/api/users/${userId}`, { 
                        headers: {
                            'Authorization' : 'Bearer testing-token',
                            'Accept': 'application/json, text/plain, */*',
                        },
                });
                responseData.value = response.data;

            } catch (error) {
                
            }
            
        }
        const formatDateTime = (str: string) => {
            const date = new Date(str);
            return (
            date.getFullYear() +
            '-' + String(date.getMonth() + 1).padStart(2, '0') +
            '-' + String(date.getDate()).padStart(2, '0') +
            ' ' + String(date.getHours()).padStart(2, '0') +
            ':' + String(date.getMinutes()).padStart(2, '0') +
            ':' + String(date.getSeconds()).padStart(2, '0') +
            '.' + String(date.getMilliseconds()).padStart(3, '0')
            );
        };

        const getNoteByUserId = async () => {
            try {
                const response = await axios.get(`http://localhost:5001/api/notes?userId=${userId}`, { 
                        headers: {
                            'Accept': 'application/json, text/plain, */*',
                        },
                });
                responseNoteData.value = response.data;

            } catch (error) {
                
            }
        }

        const deleteNote = async (id : number) => {
            try {
                await axios.delete(`http://localhost:5001/api/notes/${id}`, {
                    headers: {
                        'Authorization' : 'Bearer testing',
                        'Accept': 'application/json, text/plain, */*',
                    },
                });
                // Navigate to the NoteList page after successful deletion
                router.push({ name: 'UserDetail', params : { userId } });
                getNoteByUserId();
            } catch (error) {
                console.error('Error deleting note:', error);
            }
        };
        getUser();
        getNoteByUserId();

        return {
            getUser,
            responseData,
            getNoteByUserId,
            responseNoteData,
            formatDateTime,
            hover,
            deleteNote,
            userId
        }
    }
})
</script>

<style scoped>

</style>