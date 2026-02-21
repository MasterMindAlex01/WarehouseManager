export interface Result<T> {
  messages: string[],
  succeeded: boolean,
  data?: T
}
