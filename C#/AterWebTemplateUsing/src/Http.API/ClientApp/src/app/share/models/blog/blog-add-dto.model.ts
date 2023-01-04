export interface BlogAddDto {
  /**
   * ����
   */
  title?: string | null;
  /**
   * ��Ҫ
   */
  summary?: string | null;
  /**
   * ����
   */
  content?: string | null;
  systemUserId: string;

}
