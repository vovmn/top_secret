<template>
  <AuthLayout>
    <v-row class="h-screen pb-16" justify="center">
      <BaseColumn class-list="d-flex justify-center align-center" cols="12">
        <v-card>
          <v-row class="pt-8" justify="center">
            <h1>Регистрация</h1>
          </v-row>
          <v-card-text>
            <v-form v-model="formValid">
              <v-container width="450">
                <v-row>
                  <BaseColumn class-list="pb-0" cols="4">
                    <v-text-field
                      v-model="name"
                      color="info"
                      label="Имя"
                      :rules="[rules.required]"
                      variant="underlined"
                    />
                  </BaseColumn>
                  <BaseColumn cols="4">
                    <v-text-field
                      v-model="surname"
                      color="info"
                      label="Фамилия"
                      :rules="[rules.required]"
                      variant="underlined"
                    />
                  </BaseColumn>
                  <BaseColumn cols="4">
                    <v-text-field
                      v-model="fathername"
                      color="info"
                      label="Отчество"
                      :rules="[rules.required]"
                      variant="underlined"
                    />
                  </BaseColumn>
                  <BaseColumn cols="12">
                    <v-text-field
                      v-model="login"
                      color="info"
                      label="Логин"
                      :rules="[rules.required]"
                      variant="underlined"
                    />
                    <v-text-field
                      v-model="email"
                      color="info"
                      label="Email"
                      prepend-inner-icon="mdi-email-outline"
                      :rules="[rules.required, rules.email]"
                      variant="underlined"
                    />
                    <v-mask-input
                      v-model="phoneNumber"
                      color="info"
                      label="Телефон"
                      :mask="phoneMask"
                      prepend-inner-icon="mdi-phone-outline"
                      :rules="[rules.required, rules.phoneNumber]"
                      variant="underlined"
                    />
                    <v-text-field
                      v-model="password1"
                      :append-icon="show ? 'mdi-eye' : 'mdi-eye-off'"
                      color="info"
                      hint="Должен содержать минимум 8 символов"
                      label="Пароль"
                      placeholder="Введите пароль"
                      prepend-inner-icon="mdi-lock-outline"
                      :rules="[rules.required, rules.min]"
                      :type="show ? 'text' : 'password'"
                      variant="underlined"
                      @click:append="show = !show"
                    />
                    <v-text-field
                      v-model="password2"
                      :append-icon="show ? 'mdi-eye' : 'mdi-eye-off'"
                      color="info"
                      label="Повторите пароль"
                      placeholder="Повторите пароль"
                      :rules="[rules.required, rules.same]"
                      :type="show ? 'text' : 'password'"
                      variant="underlined"
                      @click:append="show = !show"
                    />
                  </BaseColumn>
                </v-row>
              </v-container>

              <v-divider />
              <v-card-actions>
                <v-spacer />
                <v-btn color="success" :disabled="!formValid" @click="onSubmit">
                  Завершить регистрацию
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
  import { VMaskInput } from 'vuetify/labs/VMaskInput'
  import { phoneMask } from '@/components/features/auth/constants/auth_phone_mask'
  import AuthLayout from '@/layouts/AuthLayout.vue'

  const name = ref(null)
  const surname = ref(null)
  const fathername = ref(null)
  const login = ref(null)
  const email = ref(null)
  const phoneNumber = ref('+7 ')
  const password1 = ref(null)
  const password2 = ref(null)

  const show = ref(false)

  const rules = {
    required: (value: string) => !!value || 'Заполните все поля.',
    min: (value: string | any[]) => value.length >= 8 || 'Пароль должен содержать минимум 8 символов',
    same: (value: string) => {
      return value === password1.value || 'Пароли должны совпадать'
    },
    email: (value: string) => {
      const pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
      return pattern.test(value) || 'Неверный email'
    },
    phoneNumber: (value: string) => {
      const phoneRegex = /^[\+]?[0-9\s\-\(\)]{10,}$/
      return phoneRegex.test(value.trim()) || 'Неверный номер телефона'
    },
  }

  const formValid = ref(false)

  function onSubmit () {
    if (formValid.value) {
      console.log('Форма валидна:', { name: name.value, phoneNumber: phoneNumber.value, password: password2.value })
    }
  }
</script>

<style scoped lang="sass">

</style>
