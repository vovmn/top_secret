<template>
  <AuthLayout>
    <v-row class="h-screen pb-16" justify="center">
      <BaseColumn class-list="d-flex justify-center align-center" cols="12">
        <v-card>
          <v-row class="pt-8" justify="center">
            <h1>Вход</h1>
          </v-row>
          <v-card-text>
            <v-form v-model="formValid">
              <v-container height="250" width="350">
                <v-text-field
                  v-model="formData.login"
                  color="info"
                  label="Логин"
                  :rules="[rules.required]"
                  variant="underlined"
                />
                <v-text-field
                  v-model="formData.password"
                  :append-icon="show ? 'mdi-eye' : 'mdi-eye-off'"
                  color="info"
                  hint="Должен содержать минимум 8 символов"
                  label="Пароль"
                  placeholder="Введите пароль"
                  :rules="[rules.required, rules.min]"
                  :type="show ? 'text' : 'password'"
                  variant="underlined"
                  @click:append="show = !show"
                />
              </v-container>

              <v-divider />
              <v-card-actions>
                <v-spacer />
                <v-btn color="success" :disabled="!formValid" :loading="isLoading" @click="onSubmit">
                  Войти
                  <v-icon end icon="mdi-chevron-right" />
                </v-btn>
              </v-card-actions>
            </v-form>
          </v-card-text>
        </v-card>
      </BaseColumn>
    </v-row>
  </AuthLayout>
</template>

<script setup lang="ts">
  import type { LoginForm } from './types/auth_login_types'
  import AuthLayout from '@/layouts/AuthLayout.vue'
  import { useAuthStore } from '@/stores/app'

  const router = useRouter()

  const authStore = useAuthStore()

  const formData = ref<LoginForm>({
    login: '',
    password: '',
  })

  const show = ref(false)

  const rules = {
    required: (value: string) => !!value || 'Заполните все поля.',
    min: (v: string | any[]) => v.length >= 8 || 'Пароль должен содержать минимум 8 символов',
  }

  const formValid = ref(false)
  const isLoading = ref(false)

  async function onSubmit () {
    if (!formValid.value) return
    isLoading.value = true
    await nextTick()
    try {
      const minDelay = new Promise(resolve => setTimeout(resolve, 500))
      const [result] = await Promise.all([authStore.login(formData.value), minDelay])
      if (result.success) {
        console.log('Успешный вход', authStore.username)
        router.push('/')
      } else {
        console.error('Ошибка входа', result.error)
      }
    } catch (error) {
      console.error('Критическая ошибка', error)
    }
    isLoading.value = false
  }

</script>

<style scoped lang="sass">

</style>
